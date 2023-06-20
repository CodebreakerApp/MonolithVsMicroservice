using CodeBreaker.Services.Bot.Runner.Models.Fields;

namespace CodeBreaker.Services.Bot.Runner.Mapping;

internal class ApiFieldTransferMappingVisitor : IFieldVisitor<Games.Transfer.Api.Field>
{
    public Games.Transfer.Api.Field Visit(ColorField field) =>
        new ()
        {
            Color = Enum.GetName(field.Color)
        };

    public Games.Transfer.Api.Field Visit(ColorShapeField field) =>
        new ()
        {
            Color = Enum.GetName(field.Color),
            Shape = Enum.GetName(field.Shape)
        };
}