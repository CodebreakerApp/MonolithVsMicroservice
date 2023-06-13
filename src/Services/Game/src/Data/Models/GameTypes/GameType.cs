using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.Data.Models.GameTypes;

public abstract class GameType : IGameTypeVisitable
{
    public abstract int Holes { get; }

    public abstract int MaxMoves { get; }

    public abstract IReadOnlyList<Field> PossibleFields { get; }

    public abstract TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor);
}