using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

namespace CodeBreaker.Services.Live.SignalRHubs;

internal interface ILiveHubSender
{
    Task FireGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken);
    Task FireGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken);
    Task FireMoveMadeAsync(MoveCreatedPayload payload, CancellationToken cancellationToken);
}