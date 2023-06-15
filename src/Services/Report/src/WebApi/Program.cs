using Azure.Identity;
using CodeBreaker.Services.Report.Common.Extensions;
using CodeBreaker.Services.Report.Data.DatabaseContexts;
using CodeBreaker.Services.Report.Data.Models;
using CodeBreaker.Services.Report.Data.Models.Fields;
using CodeBreaker.Services.Report.Data.Models.KeyPegs;
using CodeBreaker.Services.Report.Data.Repositories;
using CodeBreaker.Services.Report.Serialization;
using CodeBreaker.Services.Report.WebApi.Endpoints;
using CodeBreaker.Services.Report.WebApi.Extensions;
using CodeBreaker.Services.Report.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

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

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddDbContext<ReportDbContext>(dbBuilder =>
{
    dbBuilder.UseSqlServer(builder.Configuration.GetRequired("ReportService:Database:PasswordlessConnectionString"));
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
builder.Services.AddSwaggerGen(options => options.MapType<TimeSpan>(() => new() { Type = "string", Example = new OpenApiString("00:00:00") }));

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGameEndpoints();
app.MapStatisticsEndpoint();

app.Run();