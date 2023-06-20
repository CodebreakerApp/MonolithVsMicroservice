using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Services.Bot.WebApi.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class ApiMapping
{
    public static partial Transfer.Api.Bot ToTransfer(this Data.Models.Bot bot);
}
