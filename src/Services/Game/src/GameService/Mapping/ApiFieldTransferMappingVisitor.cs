using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.Mapping;

internal class ApiFieldTransferMappingVisitor : IFieldVisitor<Transfer.Api.Field>
{
    public Transfer.Api.Field Visit(ColorField field) =>
        new ()
        {
            Color = Enum.GetName(field.Color)
        };

    public Transfer.Api.Field Visit(ColorShapeField field) =>
        new ()
        {
            Color = Enum.GetName(field.Color),
            Shape = Enum.GetName(field.Shape)
        };
}