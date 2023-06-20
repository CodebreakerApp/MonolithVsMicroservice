using CodeBreaker.Services.Bot.Messaging.Args;
using CodeBreaker.Services.Bot.Messaging.AsyncEvent;
using CodeBreaker.Services.Bot.Messaging.Transfer.Payloads;

namespace CodeBreaker.Services.Bot.Messaging.Services;
public interface IBotConsumer
{
    DateTime? LastMessageReceivedAt { get; }
    TimeSpan? NoMessageDuration { get; }

    event AsyncEventHandler<BotScheduledPayload>? OnBotScheduledAsync;
    event AsyncEventHandler<OnErrorArgs>? OnBotScheduledErrorAsync;

    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}