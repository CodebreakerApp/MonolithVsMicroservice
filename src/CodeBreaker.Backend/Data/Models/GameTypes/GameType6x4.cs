using CodeBreaker.Backend.Data.Models.Fields;
using System.Collections.Immutable;

namespace CodeBreaker.Backend.Data.Models.GameTypes;

public class GameType6x4 : GameType
{
    public override int Holes { get; } = 4;

    public override int MaxMoves { get; } = 12;

    public override IReadOnlyList<Field> PossibleFields { get; } = new ImmutableArray<Field>()
        {
            new ColorField(FieldColor.Red),
            new ColorField(FieldColor.Blue),
            new ColorField(FieldColor.Green),
            new ColorField(FieldColor.Yellow),
            new ColorField(FieldColor.White),
            new ColorField(FieldColor.Blue),
        };

    public override TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor) =>
        visitor.Visit(this);
}