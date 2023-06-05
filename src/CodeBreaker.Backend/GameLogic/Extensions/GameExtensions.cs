using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.GameLogic.Extensions;

public static class GameExtensions
{
    public static bool HasEnded(this Game game) =>
        game.End != null;
}
