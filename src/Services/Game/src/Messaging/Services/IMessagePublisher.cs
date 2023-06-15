using CodeBreaker.Services.Games.Transfer.Messaging.Payloads;

namespace CodeBreaker.Services.Games.Messaging.Services;
public interface IMessagePublisher
{
    Task PublishGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken);
    Task PublishGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken);
    Task PublishMoveCreatedAsync(MoveCreatedPayload payload, CancellationToken cancellationToken);
}