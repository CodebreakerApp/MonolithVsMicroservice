namespace CodeBreaker.Services.Bot.Runner.Models.Extensions;

internal static class GameStateExtensions
{
    public static bool HasEnded(this GameState gameState) =>
        gameState == GameState.Lost ||
        gameState == GameState.Cancelled ||
        gameState == GameState.Orphaned;

    public static bool HasEnded(this Game game) =>
        game.State.HasEnded();
}
