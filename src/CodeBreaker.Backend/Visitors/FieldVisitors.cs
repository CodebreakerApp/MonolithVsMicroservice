using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.Visitors;

public class FieldToStringVisitor : IFieldVisitor<string>
{
    public string Visit(ColorField field) =>
        field.Color.ToString();

    public string Visit(ColorShapeField field) =>
        $"{field.Color}.{field.Shape}";
}

public class FieldTransferMappingVisitor : IFieldVisitor<Transfer.Field>
{
    public Transfer.Field Visit(ColorField field) =>
        new Transfer.Field()
        {
            Color = Enum.GetName(field.Color)
        };

    public Transfer.Field Visit(ColorShapeField field) =>
        new Transfer.Field()
        {
            Color = Enum.GetName(field.Color),
            Shape = Enum.GetName(field.Shape)
        };
}