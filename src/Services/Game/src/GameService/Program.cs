using Azure.Identity;
using GameService.Extensions;
using GameService.Serialization;

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

app.MapGet("/", () => "Hello from the GameService!");

app.Run();
