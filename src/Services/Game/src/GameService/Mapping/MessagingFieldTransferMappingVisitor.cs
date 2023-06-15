using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.Mapping;

internal class MessagingFieldTransferMappingVisitor : IFieldVisitor<Messaging.Transfer.Field>
{
    public Messaging.Transfer.Field Visit(ColorField field) =>
        new ()
        {
            Color = Enum.GetName(field.Color)
        };

    public Messaging.Transfer.Field Visit(ColorShapeField field) =>
        new ()
        {
            Color = Enum.GetName(field.Color),
            Shape = Enum.GetName(field.Shape)
        };
}