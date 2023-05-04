using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.GameLogic;

public abstract class MoveApplier
{
    public MoveApplier(Game game) =>
        Game = game;

    public Game Game { get; protected set; }

    public abstract void ApplyMove(Move move);
}