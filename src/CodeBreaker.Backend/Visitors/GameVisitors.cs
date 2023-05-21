using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.GameLogic;

namespace CodeBreaker.Backend.Visitors;

public class GameLogicVisitor(Game game, List<Field> guessPegs) : IGameTypeVisitor
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