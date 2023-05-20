using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.Data.Models.GameTypes;

public abstract class GameType : IGameTypeVisitable
{
    public abstract int Holes { get; }

    public abstract int MaxMoves { get; }

    public abstract List<Field> PossibleFields { get; }

    public abstract TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor);
}