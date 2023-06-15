using CodeBreaker.Services.Report.Data.Models;
using CodeBreaker.Services.Report.Data.Models.Fields;
using CodeBreaker.Services.Report.Data.Models.KeyPegs;
using CodeBreaker.Services.Report.Transfer.Api.Responses;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Services.Report.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class TransferMapping
{
    public static partial Transfer.Api.Game ToTransfer(this Game model);

    public static partial Transfer.Api.Move ToTransfer(this Move move);

    public static partial Move ToModel(this Transfer.Api.Move transfer);

    public static Field ToModel(this Transfer.Api.Field transfer)
    {
        if (transfer.Shape != null && transfer.Color != null)
            return new ColorShapeField(Enum.Parse<FieldColor>(transfer.Color), Enum.Parse<FieldShape>(transfer.Shape));

        if (transfer.Color != null)
            return new ColorField(Enum.Parse<FieldColor>(transfer.Color));

        throw new InvalidOperationException("Invalid field");
    }

    public static Transfer.Api.Field ToTransfer(this Field field) => field.Accept(new FieldTransferMappingVisitor());

    public static partial GetStatisticsResponse ToTransfer(this Statistics statistics);

    public static string ToTransfer(this KeyPeg keyPeg) => Enum.GetName(keyPeg.Color) ?? throw new InvalidOperationException("The given keypeg value does not exist");

    public static KeyPeg ToModel(this string keyPeg) => new(Enum.Parse<KeyPegColor>(keyPeg));
}