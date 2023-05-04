namespace CodeBreaker.Backend.Data.Models.Fields;

public record class ColorShapeField(FieldColor Color, FieldShape Shape) : Field
{
    public override TResult Accept<TResult>(IFieldVisitor<TResult> visitor) =>
        visitor.Visit(this);
}