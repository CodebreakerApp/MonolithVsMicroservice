using CodeBreaker.CLI.Commands;
using CodeBreaker.CLI.Infrastructure;
using CodeBreaker.Frontend.Services;
using CodeBreaker.Frontend.Services.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Spectre.Console.Cli;

var services = new ServiceCollection();
services.Configure<LiveServiceOptions>(options =>
{
    options.Url = "http://localhost:5026/live";
    options.AutomaticReconnect = true;
});
services.Configure<GameServiceOptions>(options => options.Url = "http://localhost:5026");
services.Configure<GameTypeServiceOptions>(options => options.Url = "http://localhost:5026");
services.AddSingleton<LiveService>();
services.AddHttpClient<GameService>((services, options) =>
{
    string url = services.GetRequiredService<IOptions<GameServiceOptions>>().Value.Url;
    options.BaseAddress = new (url);
});
services.AddHttpClient<GameTypeService>((services, options) =>
{
    string url = services.GetRequiredService<IOptions<GameTypeServiceOptions>>().Value.Url;
    options.BaseAddress = new (url);
});
services.AddTransient<GameCommand.Settings>();

var app = new CommandApp(new TypeRegistrar(services));
app.Configure(config =>
{
    config.AddCommand<LiveCommand>("live");
    config.AddCommand<GameCommand>("game");
});
app.Run(args);