using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.Mapping;

internal class FieldTransferMappingVisitor : IFieldVisitor<Transfer.Api.Field>
{
    public Transfer.Api.Field Visit(ColorField field) =>
        new Transfer.Api.Field()
        {
            Color = Enum.GetName(field.Color)
        };

    public Transfer.Api.Field Visit(ColorShapeField field) =>
        new Transfer.Api.Field()
        {
            Color = Enum.GetName(field.Color),
            Shape = Enum.GetName(field.Shape)
        };
}