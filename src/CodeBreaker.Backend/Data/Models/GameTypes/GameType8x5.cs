using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.Data.Models.GameTypes;

public class GameType8x5 : GameType
{
    public override int Holes { get; set; } = 5;

    public override int MaxMoves { get; set; } = 12;

    public override List<Field> PossibleFields { get; set; } = new List<Field>()
        {
            new ColorField(FieldColor.Red),
            new ColorField(FieldColor.Blue),
            new ColorField(FieldColor.Green),
            new ColorField(FieldColor.Yellow),
            new ColorField(FieldColor.White),
            new ColorField(FieldColor.Blue),
            new ColorField(FieldColor.Magenta),
            new ColorField(FieldColor.Orange),
        };

    public override TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor) =>
        visitor.Visit(this);
}