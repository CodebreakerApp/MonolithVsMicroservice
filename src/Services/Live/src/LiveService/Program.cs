using Azure.Identity;
using CodeBreaker.Services.Live.SignalRHubs;
using CodeBreaker.Services.Live.Common.Extensions;
using Microsoft.Extensions.Azure;
using CodeBreaker.Services.Live.Services;
using CodeBreaker.Services.Games.Messaging.Services;
using Azure.Messaging.ServiceBus;

DefaultAzureCredential azureCredential = new();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    Uri endpoint = new(builder.Configuration.GetRequired("AzureAppConfigurationEndpoint"));
    options.Connect(endpoint, azureCredential)
        .Select("Shared*")
        .Select("Shared*", builder.Environment.EnvironmentName)
        .Select("LiveService*")
        .Select("LiveService*", builder.Environment.EnvironmentName);
});

builder.Services.AddApplicationInsightsTelemetry(options => options.ConnectionString = builder.Configuration.GetRequired("LiveService:ApplicationInsights:ConnectionString"));

builder.Services.AddAzureClients(clientBuilder =>
{
    var serviceBusNamespace = builder.Configuration.GetRequired("LiveService:ServiceBus:Namespace");
    clientBuilder.AddServiceBusClientWithNamespace(serviceBusNamespace);
    clientBuilder.UseCredential(azureCredential);
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<ILiveHubSender, LiveHubSender>();
builder.Services.AddSingleton<IMessageSubscriber>(context => new MessageSubscriber(context.GetRequiredService<ServiceBusClient>(), "live-service"));
builder.Services.AddHostedService<MessageWorkerService>();

var app = builder.Build();

app.MapHub<LiveHub>("/live");

app.Run();
