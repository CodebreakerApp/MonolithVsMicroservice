using Azure.Identity;
using GameService.Extensions;

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

var app = builder.Build();

app.MapGet("/", () => "Hello from the GameService!");

app.Run();
