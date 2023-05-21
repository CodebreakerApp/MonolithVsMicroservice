using CodeBreaker.Backend.Mapping;
using CodeBreaker.Backend.SignalRHubs.Models;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Backend.SignalRHubs.Mappers;

[Mapper]
public static partial class LiveHubPayloadMapper
{
    public static Transfer.LivePayloads.GameCreatedPayload ToTransfer(this GameCreatedPayload payload) =>
        new(payload.Game.ToTransfer());

    public static partial Transfer.LivePayloads.GameEndedPayload ToTransfer(this GameEndedPayload payload);

    public static Transfer.LivePayloads.MoveMadePayload ToTransfer(this MoveMadePayload payload) =>
        new (payload.GameId, payload.Move.ToTransfer());
}
