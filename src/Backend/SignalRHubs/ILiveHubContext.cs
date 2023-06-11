using CodeBreaker.Transfer.LivePayloads;

namespace CodeBreaker.Backend.SignalRHubs;

public interface ILiveHubContext
{
    Task GameCreated(GameCreatedPayload payload, CancellationToken cancellationToken = default);

    Task GameEnded(GameEndedPayload payload, CancellationToken cancellationToken = default);

    Task MoveMade(MoveMadePayload payload, CancellationToken cancellationToken = default);
}