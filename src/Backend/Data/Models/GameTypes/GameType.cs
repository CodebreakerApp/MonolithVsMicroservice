using CodeBreaker.Backend.Data.Models.Fields;
using System.Collections.Immutable;

namespace CodeBreaker.Backend.Data.Models.GameTypes;

public abstract class GameType : IGameTypeVisitable
{
    public abstract int Holes { get; }

    public abstract int MaxMoves { get; }

    public abstract IReadOnlyList<Field> PossibleFields { get; }

    public abstract TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor);
}