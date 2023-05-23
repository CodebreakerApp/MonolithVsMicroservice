using CodeBreaker.CLI.Extensions;
using CodeBreaker.Frontend.Services;
using CodeBreaker.Transfer.Requests;
using CodeBreaker.Transfer.Responses;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CodeBreaker.CLI.Commands;

internal class GameCommand(IAnsiConsole Console /* PascalCased for convenience */, GameService gameService, GameTypeService gameTypeService) : AsyncCommand<GameCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<username>")]
        public string Username { get; set; } = string.Empty;

        [CommandArgument(1, "<gametype>")]
        public string GameType { get; set; } = string.Empty;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        CreateGameRequest req = new()
        {
            GameType = settings.GameType,
            Username = settings.Username,
        };

        (CreateGameResponse gameResponse, GetGameTypeResponse gameTypeResponse) = await Console.Status().StartAsync("Starting game...", async _ =>
        {
            var gameRes = await gameService.StartGame(req);
            return (gameRes, await gameTypeService.GetGameTypeAsync(gameRes.Game.Type));
        });
        int gameId = gameResponse.Game.Id;

        bool play = true;
        bool won = false;
        for (int i = 0; play; i++)
        {
            Console.MarkupLineInterpolated($"{Environment.NewLine}[underline]Move #{i+1}[/]");
            var fields = Console.PromptFields(gameTypeResponse.GameType);
            Console.WriteColorsLine(fields);
            CreateMoveRequest createMoveRequest = new() { Fields = fields.ToList() };
            var createMoveResponse = await Console.Status().StartAsync("Applying move...", _ => gameService.MakeMove(gameId, createMoveRequest));
            Console.WriteColorsLine(createMoveResponse.KeyPegs);
            play = !createMoveResponse.GameEnded;
            won = createMoveResponse.GameWon;
        }

        Console.WriteGameEnd(won);

        return 0;
    }
}