using CodeBreaker.Services.Bot.Runner.Models.Fields;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Services.Bot.Runner.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class FieldMapper
{
    public static Field ToModel(this Games.Transfer.Api.Field transfer)
    {
        if (transfer.Shape != null && transfer.Color != null)
            return new ColorShapeField(Enum.Parse<FieldColor>(transfer.Color), Enum.Parse<FieldShape>(transfer.Shape));

        if (transfer.Color != null)
            return new ColorField(Enum.Parse<FieldColor>(transfer.Color));

        throw new InvalidOperationException("Invalid field");
    }
}