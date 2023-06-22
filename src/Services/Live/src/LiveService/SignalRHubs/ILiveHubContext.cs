using CodeBreaker.Services.Live.Transfer;

namespace CodeBreaker.Services.Live.SignalRHubs;

public interface ILiveHubContext
{
    Task GameCreated(GameCreatedPayload payload, CancellationToken cancellationToken = default);

    Task GameEnded(GameEndedPayload payload, CancellationToken cancellationToken = default);

    Task MoveMade(MoveCreatedPayload payload, CancellationToken cancellationToken = default);
}
