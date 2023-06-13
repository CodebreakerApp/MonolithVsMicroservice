using CodeBreaker.Services.Games.Data.Models;

namespace CodeBreaker.Services.Games.GameLogic.Extensions;

internal static class GameExtensions
{
    public static bool HasEnded(this Game game) =>
        game.End != null;
}
