using CodeBreaker.Services.Games.Messaging.Args;
using CodeBreaker.Services.Games.Messaging.AsyncEvent;
using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

namespace CodeBreaker.Services.Games.Messaging.Services;
public interface IMessageSubscriber
{
    event AsyncEventHandler<GameCreatedPayload>? OnGameCreatedAsync;
    event AsyncEventHandler<OnErrorArgs>? OnGameCreatedErrorAsync;
    event AsyncEventHandler<GameEndedPayload>? OnGameEndedAsync;
    event AsyncEventHandler<OnErrorArgs>? OnGameEndedErrorAsync;
    event AsyncEventHandler<MoveCreatedPayload>? OnMoveCreatedAsync;
    event AsyncEventHandler<OnErrorArgs>? OnMoveCreatedErrorAsync;

    DateTime? LastMessageReceivedAt { get; }
    TimeSpan? NoMessageDuration { get; }

    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}