using CodeBreaker.Backend.SignalRHubs.Models;

namespace CodeBreaker.Backend.SignalRHubs;
public interface ILiveHubSender
{
    Task FireGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken);
    Task FireGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken);
    Task FireMoveMadeAsync(MoveMadePayload payload, CancellationToken cancellationToken);
}