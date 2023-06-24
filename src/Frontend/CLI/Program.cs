using CodeBreaker.CLI.Commands;
using CodeBreaker.CLI.Extensions;
using CodeBreaker.CLI.Infrastructure;
using CodeBreaker.Frontend.Services;
using CodeBreaker.Frontend.Services.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json", false, true);

var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
if (environment != null)
    configBuilder.AddJsonFile($"appsettings.{environment}.json", true, true);

var config = configBuilder.Build();
var services = new ServiceCollection();;
services.Configure<LiveServiceOptions>(config.GetRequiredSection("live"));
services.AddSingleton<LiveService>();
services.AddHttpClient<GameService>(options => options.BaseAddress = new (config.GetRequired("game:base")));
services.AddHttpClient<GameTypeService>(options => options.BaseAddress = new (config.GetRequired("game:base")));
services.AddHttpClient<ReportService>(options => options.BaseAddress = new(config.GetRequired("report:base")));
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