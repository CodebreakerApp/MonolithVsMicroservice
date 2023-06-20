using CodeBreaker.Services.Bot.Runner.Models;

namespace CodeBreaker.Services.Bot.Runner.BotLogic.Exceptions;

internal class GameEndedException : Exception
{
    public GameEndedException(Game game)
    {
        Game = game;
    }

    public GameEndedException(Game game, string? message) : base(message)
    {
        Game = game;
    }

    public GameEndedException(Game game, string? message, Exception? innerException) : base(message, innerException)
    {
        Game = game;
    }

    public Game Game { get; init; }

    public GameState GameState => Game.State;
}
