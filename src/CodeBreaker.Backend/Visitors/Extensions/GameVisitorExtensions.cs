using CodeBreaker.Backend.Visitors;

namespace CodeBreaker.Backend.Data.Models;

public static class GameVisitorExtensions
{
    public static void ApplyMove(this Game game, Move move) =>
        game.Type.Accept(new GameLogicVisitor(game, move));
}
