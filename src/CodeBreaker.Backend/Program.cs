using CodeBreaker.Backend.Data.Repositories;
using CodeBreaker.Backend.Services;
using Transfer = CodeBreaker.Transfer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Logging.AddConsole();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.AddContext<AppJsonSerializerContext>();
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
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
