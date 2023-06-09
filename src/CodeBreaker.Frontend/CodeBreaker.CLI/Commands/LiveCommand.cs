using CodeBreaker.CLI.Extensions;
using CodeBreaker.Frontend.Services;
using CodeBreaker.Transfer.LivePayloads;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CodeBreaker.CLI.Commands;

internal class LiveCommand : AsyncCommand
{
    private readonly IAnsiConsole Console; // PascalCased for convenience

    private readonly LiveService _liveService;

    public LiveCommand(IAnsiConsole console, LiveService liveService)
    {
        Console = console;
        _liveService = liveService;
        _liveService.OnGameCreated += OnGameCreated;
        _liveService.OnGameEnded += OnGameEnded;
        _liveService.OnMoveMade += OnMoveMade;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        await Console.Status().StartAsync("Connecting ...", _ => _liveService.ConnectAsync());

        Console.WriteLine("Press Q to quit");
        Console.WaitForKey(ConsoleKey.Q);

        await Console.Status().StartAsync("Disconnecting ...", _ => _liveService.DisconnectAsync());

        return 0;
    }

    private void OnGameCreated(object? sender, GameCreatedPayload e)
    {
        Console.MarkupLineInterpolated($"[bold]{e.Game.Username}[/] startet a {e.Game.Type}-game ({e.Game.Id})");
    }

    private void OnGameEnded(object? sender, GameEndedPayload e)
    {
        var wonText = e.Won ? string.Empty : "not ";
        Console.MarkupLineInterpolated($"Game ([bold]{e.GameId}[/]) ended at {e.End} and was {wonText}won");
    }

    private void OnMoveMade(object? sender, MoveMadePayload e)
    {
        Console.Write("Move ");
        Console.WriteColors(e.Move.Fields);
        Console.Write($" was made for game ({e.GameId}) - ");
        Console.WriteColorsLine(e.Move.KeyPegs);
    }
}