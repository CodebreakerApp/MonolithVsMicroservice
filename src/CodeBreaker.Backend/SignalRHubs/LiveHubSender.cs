using CodeBreaker.Backend.SignalRHubs.Mappers;
using CodeBreaker.Backend.SignalRHubs.Models;
using Microsoft.AspNetCore.SignalR;

namespace CodeBreaker.Backend.SignalRHubs;

public class LiveHubSender(IHubContext<LiveHub, ILiveHubContext> hubContext) : ILiveHubSender
{
    public Task FireGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.SendGameCreatedAsync(payload.ToTransfer(), cancellationToken);

    public Task FireGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.SendGameEndedAsync(payload.ToTransfer(), cancellationToken);

    public Task FireMoveMadeAsync(MoveMadePayload payload, CancellationToken cancellationToken) =>
        hubContext.Clients.All.SendMoveMadeAsync(payload.ToTransfer(), cancellationToken);
}