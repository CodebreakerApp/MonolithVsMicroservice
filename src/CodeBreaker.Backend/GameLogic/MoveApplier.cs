using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.GameLogic;

public abstract class MoveApplier
{
    public MoveApplier(Game game) =>
        Game = game;

    public Game Game { get; protected set; }

    public abstract void ApplyMove(in List<Field> guessPegs);
}