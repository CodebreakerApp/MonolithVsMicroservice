using CodeBreaker.Services.Games.Messaging.Args;
using CodeBreaker.Services.Games.Transfer.Messaging.Payloads;

namespace CodeBreaker.Services.Games.Messaging.Services;
public interface IMessageSubscriber
{
    event Func<GameCreatedPayload, Task>? OnGameCreatedAsync;
    event Func<OnErrorArgs, Task>? OnGameCreatedErrorAsync;
    event Func<GameEndedPayload, Task>? OnGameEndedAsync;
    event Func<OnErrorArgs, Task>? OnGameEndedErrorAsync;
    event Func<MoveCreatedPayload, Task>? OnMoveCreatedAsync;
    event Func<OnErrorArgs, Task>? OnMoveCreatedErrorAsync;

    DateTime? LastMessageReceivedAt { get; }
    TimeSpan? NoMessageDuration { get; }

    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}