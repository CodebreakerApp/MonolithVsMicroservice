using Azure.Identity;
using CodeBreaker.Services.Games.Data.DatabaseContexts;
using CodeBreaker.Services.Games.Data.Repositories;
using CodeBreaker.Services.Games.Endpoints;
using CodeBreaker.Services.Games.Extensions;
using CodeBreaker.Services.Games.Services;
using GameService.Serialization;
using Microsoft.EntityFrameworkCore;

DefaultAzureCredential azureCredential = new();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    Uri endpoint = new(builder.Configuration.GetRequired("AzureAppConfigurationEndpoint"));
    options.Connect(endpoint, azureCredential)
        .Select("Shared:*")
        .Select("Shared:*", builder.Environment.EnvironmentName)
        .Select("GameService:*")
        .Select("GameService:*", builder.Environment.EnvironmentName);
});

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddDbContext<GamesDbContext>(dbBuilder =>
{
    dbBuilder.UseSqlServer(builder.Configuration.GetRequired("GameService:Database:PasswordlessConnectionString"));
#if DEBUG
    dbBuilder.EnableSensitiveDataLogging();
#endif
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.TypeInfoResolver = new ApiJsonSerializerContext();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGameEndpoints();
app.MapGameTypeEndpoints();

app.Run();
