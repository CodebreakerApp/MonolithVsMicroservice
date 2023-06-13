using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.GameLogic;

internal abstract class MoveApplier
{
    public MoveApplier(Game game) =>
        Game = game;

    public Game Game { get; protected set; }

    public abstract Move ApplyMove(in IReadOnlyList<Field> guessPegs);
}