using CodeBreaker.Services.Bot.Runner.Models.GameTypes;

namespace CodeBreaker.Services.Bot.Runner.Services.Args;

internal class CreateGameArgs
{
    public required GameType Type { get; init; }

    public required string Username { get; init; }
}