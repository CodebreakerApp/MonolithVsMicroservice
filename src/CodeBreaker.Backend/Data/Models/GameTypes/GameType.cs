using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.Data.Models.GameTypes;

public abstract class GameType : IGameTypeVisitable
{
    public abstract int Holes { get; set; }

    public abstract int MaxMoves { get; set; }

    public abstract List<Field> PossibleFields { get; set; }

    public abstract TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor);
}