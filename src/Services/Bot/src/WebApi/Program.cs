using Azure.Identity;
using CodeBreaker.Services.Bot.Data.DatabaseContexts;
using CodeBreaker.Services.Bot.Data.Repositories;
using CodeBreaker.Services.Bot.Messaging.Services;
using CodeBreaker.Services.Bot.WebApi.Endpoints;
using CodeBreaker.Services.Bot.WebApi.Serialization;
using CodeBreaker.Services.Bot.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

DefaultAzureCredential azureCredential = new();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    Uri endpoint = new(builder.Configuration.GetRequired("AzureAppConfigurationEndpoint"));
    options.Connect(endpoint, azureCredential)
        .Select("Shared*")
        .Select("Shared*", builder.Environment.EnvironmentName)
        .Select("BotService*")
        .Select("BotService*", builder.Environment.EnvironmentName);
});

builder.Services.AddApplicationInsightsTelemetry(options => options.ConnectionString = builder.Configuration.GetRequired("BotService:ApplicationInsights:ConnectionString"));

builder.Services.AddDbContext<BotDbContext>(dbBuilder =>
{
    dbBuilder.UseSqlServer(builder.Configuration.GetRequired("BotService:Database:PasswordlessConnectionString"));
#if DEBUG
    dbBuilder.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddAzureClients(clientBuilder =>
{
    var serviceBusNamespace = builder.Configuration.GetRequired("BotService:ServiceBus:Namespace");
    clientBuilder.AddServiceBusClientWithNamespace(serviceBusNamespace);
    clientBuilder.UseCredential(azureCredential);
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.TypeInfoResolver = new ApiJsonSerializerContext();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBotRepository, BotRepository>();
builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddScoped<IBotFactory, BotFactory>();
builder.Services.AddSingleton<IBotScheduler, BotScheduler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapBotEndpoints();

app.Run();
