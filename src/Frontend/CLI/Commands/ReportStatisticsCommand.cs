using CodeBreaker.Frontend.Services;
using CodeBreaker.Services.Report.Transfer.Api.Requests;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CodeBreaker.CLI.Commands;

internal class ReportStatisticsCommand(IAnsiConsole Console /* PascalCased for convenience */, ReportService reportService) : AsyncCommand<ReportStatisticsCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-f|--from")]
        public DateTime From { get; set; } = default;

        [CommandOption("-t|--to")]
        public DateTime To { get; set; } = DateTime.Today.AddDays(1);

        [CommandOption("-g|--gametype")]
        public string? GameType { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var args = new GetStatisticsRequest()
        {
            From = settings.From,
            To = settings.To,
            GameType = settings.GameType
        };
        var result = await Console.Status().StartAsync("Requesting statistics...", _ => reportService.GetStatisticsAsync(args));

        Console.WriteLine("Statistics", new Style(decoration: Decoration.Underline));
        Console.WriteLine();

        var chart = new BreakdownChart()
            .FullSize()
            .AddItem("Won games", result.WonGames, Color.Green)
            .AddItem("Lost games", result.TotalGames - result.WonGames - result.CancelledGames, Color.Yellow)
            .AddItem("Cancelled games", result.CancelledGames, Color.Red);

        var grid = new Grid().AddColumns(2)
            .AddRow("Total games:", result.TotalGames.ToString())
            .AddEmptyRow()
            .AddRow("Minimum game duration:", result.MinGameDuration.ToString())
            .AddRow("Average game duration:", result.AvgGameDuration.ToString())
            .AddRow("Maximum game duration:", result.MaxGameDuration.ToString())
            .AddEmptyRow()
            .AddRow("Minimum move count:", result.MinMoveCount.ToString())
            .AddRow("Average move count:", result.AvgMoveCount.ToString())
            .AddRow("Maximum move count:", result.MaxMoveCount.ToString());
        
        Console.Write(chart);
        Console.Write(grid);
        return 0;
    }
}
