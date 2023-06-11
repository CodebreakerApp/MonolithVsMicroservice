using CodeBreaker.Backend.SignalRHubs.Mappers;
using CodeBreaker.Backend.SignalRHubs.Models;
using Microsoft.AspNetCore.SignalR;

namespace CodeBreaker.Backend.SignalRHubs;

public class LiveHubSender(IHubContext<LiveHub, ILiveHubContext> hubContext) : ILiveHubSender
{
    public Task FireGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.GameCreated(payload.ToTransfer(), cancellationToken);

    public Task FireGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.GameEnded(payload.ToTransfer(), cancellationToken);

    public Task FireMoveMadeAsync(MoveMadePayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.MoveMade(payload.ToTransfer(), cancellationToken);
}