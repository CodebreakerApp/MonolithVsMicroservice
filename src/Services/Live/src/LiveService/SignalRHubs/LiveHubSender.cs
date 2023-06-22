using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;
using CodeBreaker.Services.Live.SignalRHubs.Mappers;
using Microsoft.AspNetCore.SignalR;

namespace CodeBreaker.Services.Live.SignalRHubs;

internal class LiveHubSender(IHubContext<LiveHub, ILiveHubContext> hubContext) : ILiveHubSender
{
    public Task FireGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.GameCreated(payload.ToTransfer(), cancellationToken);

    public Task FireGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.GameEnded(payload.ToTransfer(), cancellationToken);

    public Task FireMoveMadeAsync(MoveCreatedPayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.MoveMade(payload.ToTransfer(), cancellationToken);
}