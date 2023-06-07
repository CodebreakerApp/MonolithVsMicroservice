using CodeBreaker.Backend.Data.Repositories;
using CodeBreaker.Backend.Services;
using Transfer = CodeBreaker.Transfer;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using CodeBreaker.Backend.Data.DatabaseContexts;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using CodeBreaker.Backend.SignalRHubs;
using CodeBreaker.Backend.Endpoints;
using CodeBreaker.Backend.BotLogic;
using Microsoft.OpenApi.Any;

DefaultAzureCredential azureCredential = new();
var builder = WebApplication.CreateSlimBuilder(args);
builder.Logging.AddConsole();
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration.GetRequired("AzureApplicationInsightsConnectionString");
});

builder.Configuration.AddAzureAppConfiguration(options =>
{
    Uri endpoint = new (builder.Configuration["AzureAppConfigurationEndpoint"] ?? "https://codebreaker-mono-config.azconfig.io");
    options.Connect(endpoint, azureCredential)
        .Select(KeyFilter.Any, LabelFilter.Null)
        .Select(KeyFilter.Any, builder.Environment.EnvironmentName);
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.AddContext<AppJsonSerializerContext>();
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Database
builder.Services.AddDbContext<CodeBreakerDbContext>(dbBuilder =>
{
    var passwordlessConnectionString = builder.Configuration.GetRequired("AzureSqlPasswordlessConnectionString");
    dbBuilder.UseSqlServer(passwordlessConnectionString);
#if DEBUG
    dbBuilder.EnableSensitiveDataLogging();
#endif
});

// SignalR
builder.Services.AddSignalR();

// Endpoint documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseOneOfForPolymorphism();
    options.MapType<TimeSpan>(() => new () { Type = "string", Example = new OpenApiString("00:00:00") });
});

// Cache
builder.Services.AddMemoryCache();

// Application services
builder.Services.AddScoped<IGameRepository, EfcoreSqlGameRepository>();
builder.Services.AddScoped<IGameTypeRepository, GameTypeRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IMoveService, MoveService>();
builder.Services.AddScoped<IGameTypeService, GameTypeService>();
builder.Services.AddScoped<ILiveHubSender, LiveHubSender>();
builder.Services.AddScoped<IBotRepository, BotRepository>();
builder.Services.AddTransient<IBotRunnerService, BotRunnerService>();
builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddSingleton<IBotFactory, BotFactory>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddRequestDecompression();

var app = builder.Build();

app.UseRequestDecompression();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGameEndpoints();
app.MapGameTypeEndpoints();
app.MapBotEndpoints();
app.MapBotTypeEndpoints();
app.MapReportEndpoints();
app.MapHub<LiveHub>("/live");

app.Run();

[JsonSerializable(typeof(Transfer.Game))]
[JsonSerializable(typeof(Transfer.Bot))]
[JsonSerializable(typeof(Transfer.GameType))]
[JsonSerializable(typeof(Transfer.Requests.CreateGameRequest))]
[JsonSerializable(typeof(Transfer.Requests.CreateMoveRequest))]
[JsonSerializable(typeof(Transfer.Requests.CreateBotRequest))]
[JsonSerializable(typeof(Transfer.Responses.CreateGameResponse))]
[JsonSerializable(typeof(Transfer.Responses.CreateMoveResponse))]
[JsonSerializable(typeof(Transfer.Responses.CreateBotResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetBotTypesResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGamesResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameTypeResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameTypesResponse))]
internal partial class AppJsonSerializerContext : JsonSerializerContext { }