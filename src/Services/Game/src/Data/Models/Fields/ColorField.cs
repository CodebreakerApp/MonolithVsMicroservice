namespace CodeBreaker.Services.Games.Data.Models.Fields;

public record class ColorField(FieldColor Color) : Field
{
    public override TResult Accept<TResult>(IFieldVisitor<TResult> visitor) =>
        visitor.Visit(this);
}