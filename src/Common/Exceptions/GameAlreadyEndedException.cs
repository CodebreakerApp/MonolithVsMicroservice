using System.Runtime.Serialization;

namespace CodeBreaker.Common.Exceptions;

public class GameAlreadyEndedException : Exception
{
    public GameAlreadyEndedException(int gameId) : base($"The game with the {gameId} has already ended.")
    {
    }

    public int GameId { get; private init; }
}
