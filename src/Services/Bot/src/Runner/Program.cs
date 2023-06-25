using Azure.Identity;
using CodeBreaker.Services.Bot.Data.DatabaseContexts;
using CodeBreaker.Services.Bot.Data.Repositories;
using CodeBreaker.Services.Bot.Messaging.Services;
using CodeBreaker.Services.Bot.Runner.Options;
using CodeBreaker.Services.Bot.Runner.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Resilience;

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
                .Select("BotService*")
                .Select("BotService*", context.HostingEnvironment.EnvironmentName);
        });
    })
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService(options => options.ConnectionString = context.Configuration.GetRequired("BotService:ApplicationInsights:ConnectionString"));
        services.AddAzureClients(clientBuilder =>
        {
            var serviceBusNamespace = context.Configuration.GetRequired("BotService:ServiceBus:Namespace");
            clientBuilder.AddServiceBusClientWithNamespace(serviceBusNamespace);
            clientBuilder.UseCredential(azureCredential);
        });
        services.AddDbContext<BotDbContext>(dbBuilder =>
        {
            dbBuilder.UseSqlServer(context.Configuration.GetRequired("BotService:Database:PasswordlessConnectionString"));
#if DEBUG
            dbBuilder.EnableSensitiveDataLogging();
#endif
        });

        services.Configure<BotScheduleWorkerOptions>(context.Configuration.GetSection("BotService:BotScheduleWorker"));
        services.AddScoped<IBotRepository, BotRepository>();
        services.AddScoped<IBotConsumer, BotConsumer>();
        services.AddHttpClient<IGameTypeService, GameTypeService>(options =>
        {
            options.BaseAddress = new(context.Configuration.GetRequired("BotService:GameTypeBaseAddress"));
        }).AddStandardResilienceHandler();
        services.AddHttpClient<IGameService, GameService>(options =>
        {
            options.BaseAddress = new(context.Configuration.GetRequired("BotService:GameBaseAddress"));
        }).AddStandardResilienceHandler();
        services.AddHttpClient<IMoveService, MoveService>(options =>
        {
            options.BaseAddress = new(context.Configuration.GetRequired("BotService:GameBaseAddress"));
        }).AddStandardResilienceHandler();
        services.AddSingleton<BotScheduleWorker>();
    })
    .Build();

await host.Services
    .GetRequiredService<BotScheduleWorker>()
    .RunAsync();