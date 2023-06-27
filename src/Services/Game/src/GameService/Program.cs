using Azure.Identity;
using CodeBreaker.Services.Games.Data.DatabaseContexts;
using CodeBreaker.Services.Games.Data.Repositories;
using CodeBreaker.Services.Games.Endpoints;
using CodeBreaker.Services.Games.Extensions;
using CodeBreaker.Services.Games.Messaging.Services;
using CodeBreaker.Services.Games.Services;
using GameService.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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
        .Select("GameService*")
        .Select("GameService*", builder.Environment.EnvironmentName);
});

builder.Services.AddApplicationInsightsTelemetry(options => options.ConnectionString = builder.Configuration.GetRequired("GameService:ApplicationInsights:ConnectionString"));

builder.Services.AddDbContext<GamesDbContext>(dbBuilder =>
{
    dbBuilder.UseSqlServer(builder.Configuration.GetRequired("GameService:Database:PasswordlessConnectionString"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
#if DEBUG
    dbBuilder.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddAzureClients(clientBuilder =>
{
    var serviceBusNamespace = builder.Configuration.GetRequired("GameService:ServiceBus:Namespace");
    clientBuilder.AddServiceBusClientWithNamespace(serviceBusNamespace);
    clientBuilder.UseCredential(azureCredential);
});

builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>(); builder.Services.AddHealthChecks()
    .AddDbContextCheck<GamesDbContext>("Ready", tags: new[] { "ready" });

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.TypeInfoResolver = new ApiJsonSerializerContext();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameTypeRepository,  GameTypeRepository>();
builder.Services.AddScoped<IGameService, CodeBreaker.Services.Games.Services.GameService>();
builder.Services.AddScoped<IGameTypeService, GameTypeService>();
builder.Services.AddScoped<IMoveService, MoveService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGameEndpoints();
app.MapGameTypeEndpoints();
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("ready")
});
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});

app.Run();
