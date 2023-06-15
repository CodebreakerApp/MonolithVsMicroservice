using Azure.Identity;
using Azure.Messaging.ServiceBus;
using CodeBreaker.Service.Report.MessageWorker.Options;
using CodeBreaker.Service.Report.MessageWorker.Services;
using CodeBreaker.Services.Games.Messaging.Services;
using CodeBreaker.Services.Report.Common.Extensions;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

DefaultAzureCredential azureCredential = new();
await Host.CreateDefaultBuilder(args)
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
    .ConfigureServices((context, services)=>
    {
        services.Configure<MessageServiceOptions>(context.Configuration.GetSection("ReportService:MessageWorker"));
        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddAzureClients(clientBuilder =>
        {
            var serviceBusNamespace = context.Configuration.GetRequired("ReportService:ServiceBus:Namespace");
            clientBuilder.AddServiceBusClientWithNamespace(serviceBusNamespace);
            clientBuilder.UseCredential(azureCredential);
        });
        services.AddSingleton<IMessageSubscriber>(services => new MessageSubscriber(services.GetRequiredService<ServiceBusClient>(), "report-service"));
        services.AddSingleton<MessageService>();
    })
    .Build()
    .Services.GetRequiredService<MessageService>()
    .RunAsync();