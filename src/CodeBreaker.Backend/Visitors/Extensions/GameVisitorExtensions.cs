using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Visitors;

namespace CodeBreaker.Backend.Data.Models;

public static class GameVisitorExtensions
{
    public static Move ApplyMove(this Game game, List<Field> guessPegs) =>
        game.Type.Accept(new GameLogicVisitor(game, guessPegs));
}
