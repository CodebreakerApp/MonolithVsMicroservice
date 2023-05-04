using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.Data.Models.GameTypes;

public class GameType6x4 : GameType
{
    public override int Holes { get; set; } = 4;

    public override int MaxMoves { get; set; } = 12;

    public override List<Field> PossibleFields { get; set; } = new List<Field>()
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