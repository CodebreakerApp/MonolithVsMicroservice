using CodeBreaker.Transfer.LivePayloads;

namespace CodeBreaker.Backend.SignalRHubs;

public interface ILiveHubContext
{
    Task SendGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken = default);

    Task SendGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken = default);

    Task SendMoveMadeAsync(MoveMadePayload payload, CancellationToken cancellationToken = default);
}