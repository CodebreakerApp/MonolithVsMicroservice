using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.Data.Models.GameTypes;

public class GameType6x4 : GameType
{
    public override int Holes { get; } = 4;

    public override int MaxMoves { get; } = 12;

    public override IReadOnlyList<Field> PossibleFields { get; } = new Field[]
        {
            new ColorField(FieldColor.Red),
            new ColorField(FieldColor.Blue),
            new ColorField(FieldColor.Green),
            new ColorField(FieldColor.Yellow),
            new ColorField(FieldColor.White),
            new ColorField(FieldColor.Black),
        };

    public override TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor) =>
        visitor.Visit(this);
}