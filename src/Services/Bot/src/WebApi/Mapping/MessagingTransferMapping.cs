using CodeBreaker.Services.Bot.Messaging.Transfer.Payloads;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Services.Bot.WebApi.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class MessagingTransferMapping
{
    [MapProperty(nameof(Data.Models.Bot.Name), nameof(BotScheduledPayload.Username))]
    public static partial BotScheduledPayload ToBotScheduledPayload(this Data.Models.Bot bot);
}
