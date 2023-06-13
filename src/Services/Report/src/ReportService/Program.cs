using Azure.Identity;
using CodeBreaker.Services.Report.Extensions;

DefaultAzureCredential azureCredential = new ();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    Uri endpoint = new(builder.Configuration.GetRequired("AzureAppConfigurationEndpoint"));
    options.Connect(endpoint, azureCredential)
        .Select("Shared*")
        .Select("Shared*", builder.Environment.EnvironmentName)
        .Select("ReportService*")
        .Select("ReportService*", builder.Environment.EnvironmentName);
});

var app = builder.Build();

app.MapGet("/", () => "Hello from the report service!");

app.Run();
