using CodeBreaker.Services.Bot.Runner.Models.Fields;

namespace CodeBreaker.Services.Bot.Runner.Models.GameTypes;

public abstract class GameType : IGameTypeVisitable
{
    public required int Holes { get; init; }

    public required int MaxMoves { get; init; }

    public required IReadOnlyList<Field> PossibleFields { get; init; }

    public abstract TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor);
}