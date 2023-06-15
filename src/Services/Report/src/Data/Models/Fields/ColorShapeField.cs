namespace CodeBreaker.Services.Report.Data.Models.Fields;

public record class ColorShapeField(FieldColor Color, FieldShape Shape) : ColorField(Color)
{
    public override TResult Accept<TResult>(IFieldVisitor<TResult> visitor) =>
        visitor.Visit(this);
}