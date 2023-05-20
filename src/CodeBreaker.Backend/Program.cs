using CodeBreaker.Backend.Data.Repositories;
using CodeBreaker.Backend.Services;
using Transfer = CodeBreaker.Transfer;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using CodeBreaker.Backend.Data.DatabaseContexts;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

DefaultAzureCredential azureCredential = new();
var builder = WebApplication.CreateSlimBuilder(args);
builder.Logging.AddConsole();

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
});

// Endpoint documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.UseOneOfForPolymorphism());

// Cache
builder.Services.AddMemoryCache();

// Application services
builder.Services.AddScoped<IGameRepository, InMemoryGameRepository>();
builder.Services.AddScoped<IGameTypeRepository, GameTypeRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IMoveService, MoveService>();
builder.Services.AddScoped<IGameTypeService, GameTypeService>();

builder.Services.AddRequestDecompression();

var app = builder.Build();

app.UseRequestDecompression();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGameEndpoints();

app.Run();

[JsonSerializable(typeof(Transfer.Game))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
