using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Services.Live.SignalRHubs.Mappers;

[Mapper]
internal static partial class LiveHubPayloadMapper
{
    public static partial Transfer.GameCreatedPayload ToTransfer(this GameCreatedPayload payload);

    public static partial Transfer.GameEndedPayload ToTransfer(this GameEndedPayload payload);

    public static partial Transfer.MoveCreatedPayload ToTransfer(this MoveCreatedPayload payload);
}
