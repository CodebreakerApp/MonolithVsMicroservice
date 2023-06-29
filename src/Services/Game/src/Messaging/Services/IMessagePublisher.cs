using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

namespace CodeBreaker.Services.Games.Messaging.Services;
public interface IMessagePublisher
{
    bool IsClosed { get; }
    Task PublishGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken);
    Task PublishGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken);
    Task PublishMoveCreatedAsync(MoveCreatedPayload payload, CancellationToken cancellationToken);
}