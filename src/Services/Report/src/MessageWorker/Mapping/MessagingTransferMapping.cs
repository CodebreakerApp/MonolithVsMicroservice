using Transfer = CodeBreaker.Services.Games.Messaging.Transfer;
using CodeBreaker.Services.Report.Data.Models;
using Riok.Mapperly.Abstractions;
using CodeBreaker.Services.Report.Data.Models.Fields;
using CodeBreaker.Services.Report.Data.Models.KeyPegs;
using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

namespace CodeBreaker.Services.Report.MessageWorker.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class MessagingTransferMapping
{
    public static partial Game ToModel(this GameCreatedPayload transfer);

    public static partial void ToModel(this GameEndedPayload transfer, Game gameToMapInto);

    [MapProperty(nameof(MoveCreatedPayload.MoveId), nameof(Move.Id))]
    public static partial Move ToMoveModel(this MoveCreatedPayload transfer);

    private static Field ToModel(this Transfer.Field transfer)
    {
        if (transfer.Shape != null && transfer.Color != null)
            return new ColorShapeField(Enum.Parse<FieldColor>(transfer.Color), Enum.Parse<FieldShape>(transfer.Shape));

        if (transfer.Color != null)
            return new ColorField(Enum.Parse<FieldColor>(transfer.Color));

        throw new InvalidOperationException("Invalid field");
    }

    private static KeyPeg ToModel(this string keyPeg) => new(Enum.Parse<KeyPegColor>(keyPeg));
}
