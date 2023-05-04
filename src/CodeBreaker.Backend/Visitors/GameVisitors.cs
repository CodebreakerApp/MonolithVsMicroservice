using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.GameLogic;

namespace CodeBreaker.Backend.Visitors;

public class GameLogicVisitor(Game game, Move moveToApply) : IGameTypeVisitor
{
    public Empty Visit(GameType6x4 gameType)
    {
        ThrowWhenGameTypeMismatch(gameType);
        new DefaultMoveApplier(game).ApplyMove(moveToApply);
        return new Empty();
    }

    public Empty Visit(GameType8x5 gameType)
    {
        ThrowWhenGameTypeMismatch(gameType);
        new DefaultMoveApplier(game).ApplyMove(moveToApply);
        return new Empty();
    }

    private void ThrowWhenGameTypeMismatch(GameType gameType)
    {
        if (gameType.GetType() != game.Type.GetType())
            throw new InvalidOperationException("The given gameType does not match the gameType of the given game!");
    }
}