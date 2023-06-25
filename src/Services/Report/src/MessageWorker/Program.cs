using Azure.Identity;
using Azure.Messaging.ServiceBus;
using CodeBreaker.Services.Games.Messaging.Services;
using CodeBreaker.Services.Report.Common.Extensions;
using CodeBreaker.Services.Report.Data.DatabaseContexts;
using CodeBreaker.Services.Report.Data.Repositories;
using CodeBreaker.Services.Report.MessageWorker.Options;
using CodeBreaker.Services.Report.MessageWorker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

DefaultAzureCredential azureCredential = new();
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        var existingConfiguration = config.Build();
        config.AddAzureAppConfiguration(options =>
        {
            Uri endpoint = new(existingConfiguration.GetRequired("AzureAppConfigurationEndpoint"));
            options.Connect(endpoint, azureCredential)
                .Select("Shared*")
                .Select("Shared*", context.HostingEnvironment.EnvironmentName)
                .Select("ReportService*")
                .Select("ReportService*", context.HostingEnvironment.EnvironmentName);
        });
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<MessageServiceOptions>(context.Configuration.GetSection("ReportService:MessageWorker"));
        services.AddApplicationInsightsTelemetryWorkerService(options => options.ConnectionString = context.Configuration.GetRequired("ReportService:ApplicationInsights:ConnectionString"));
        services.AddAzureClients(clientBuilder =>
        {
            var serviceBusNamespace = context.Configuration.GetRequired("ReportService:ServiceBus:Namespace");
            clientBuilder.AddServiceBusClientWithNamespace(serviceBusNamespace);
            clientBuilder.UseCredential(azureCredential);
        });
        services.AddDbContext<ReportDbContext>(dbBuilder =>
        {
            dbBuilder.UseSqlServer(context.Configuration.GetRequired("ReportService:Database:PasswordlessConnectionString"), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
#if DEBUG
            dbBuilder.EnableSensitiveDataLogging();
#endif
        }, ServiceLifetime.Transient);
        services.AddTransient<IGameRepository, GameRepository>();
        services.AddSingleton<IMessageSubscriber>(services => new MessageSubscriber(services.GetRequiredService<ServiceBusClient>(), "report-service"));
        services.AddSingleton<MessageService>();
    })
    .Build();

await host.Services
    .GetRequiredService<MessageService>()
    .RunAsync();