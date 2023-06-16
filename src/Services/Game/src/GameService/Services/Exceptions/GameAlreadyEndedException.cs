namespace CodeBreaker.Services.Games.Services.Exceptions;

internal class GameAlreadyEndedException : Exception
{
    public GameAlreadyEndedException(Guid gameId) : base($"The game with the {gameId} has already ended.")
    {
        GameId = gameId;
    }

    public GameAlreadyEndedException(string? message) : base(message)
    {
    }

    public GameAlreadyEndedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public Guid GameId { get; init; }
}
