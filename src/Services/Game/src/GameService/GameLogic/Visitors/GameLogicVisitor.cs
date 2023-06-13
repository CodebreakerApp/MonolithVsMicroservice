using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.Fields;
using CodeBreaker.Services.Games.Data.Models.GameTypes;

namespace CodeBreaker.Services.Games.GameLogic.Visitors;

internal class GameLogicVisitor(Game game, IReadOnlyList<Field> guessPegs) : IGameTypeVisitor<Move>
{
    public Move Visit(GameType6x4 gameType)
    {
        ThrowWhenGameTypeMismatch(gameType);
        return new DefaultMoveApplier(game).ApplyMove(guessPegs);
    }

    public Move Visit(GameType8x5 gameType)
    {
        ThrowWhenGameTypeMismatch(gameType);
        return new DefaultMoveApplier(game).ApplyMove(guessPegs);
    }

    private void ThrowWhenGameTypeMismatch(GameType gameType)
    {
        if (gameType.GetType() != game.Type.GetType())
            throw new InvalidOperationException("The given gameType does not match the gameType of the given game!");
    }
}

internal static class GameLogicVisitorExtensions
{
    public static Move ApplyMove(this Game game, IReadOnlyList<Field> guessPegs) =>
        game.Type.Accept(new GameLogicVisitor(game, guessPegs));
}