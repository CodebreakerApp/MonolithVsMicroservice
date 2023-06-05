using CodeBreaker.CLI.Commands;
using CodeBreaker.CLI.Infrastructure;
using CodeBreaker.Frontend.Services;
using CodeBreaker.Frontend.Services.Options;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

Uri baseUrl = new ("http://localhost:5026");
var services = new ServiceCollection();
services.Configure<LiveServiceOptions>(options =>
{
    options.Url = $"{baseUrl}/live";
    options.AutomaticReconnect = true;
});
services.AddSingleton<LiveService>();
services.AddHttpClient<GameService>(options => options.BaseAddress = baseUrl);
services.AddHttpClient<GameTypeService>(options => options.BaseAddress = baseUrl);
services.AddHttpClient<ReportService>(options => options.BaseAddress = baseUrl);
services.AddTransient<GameCommand.Settings>();
services.AddTransient<ReportStatisticsCommand.Settings>();

var app = new CommandApp(new TypeRegistrar(services));
app.Configure(config =>
{
    config.AddCommand<LiveCommand>("live");
    config.AddCommand<GameCommand>("game");
    config.AddCommand<ReportStatisticsCommand>("report");
});
app.Run(args);