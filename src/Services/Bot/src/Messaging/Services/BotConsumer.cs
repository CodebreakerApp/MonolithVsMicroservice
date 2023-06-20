using Azure.Messaging.ServiceBus;
using CodeBreaker.Services.Bot.Messaging.Args;
using CodeBreaker.Services.Bot.Messaging.AsyncEvent;
using CodeBreaker.Services.Bot.Messaging.Transfer.Payloads;
using MemoryPack;

namespace CodeBreaker.Services.Bot.Messaging.Services;

/// <remarks>
/// Recommended lifetime: Singleton<br/>
/// Threadsafe: Yes
/// </remarks>
public class BotConsumer : IBotConsumer
{
    private readonly ServiceBusProcessor _botScheduledProcessor;

    public BotConsumer(ServiceBusClient serviceBusClient)
    {
        _botScheduledProcessor = serviceBusClient.CreateProcessor(MessageQueueNames.ScheduledBBots);
        RegisterMessageCallback();
        RegisterErrorCallback();
    }

    public DateTime? LastMessageReceivedAt { get; private set; }

    public TimeSpan? NoMessageDuration => DateTime.Now - LastMessageReceivedAt;

    public event AsyncEventHandler<BotScheduledPayload>? OnBotScheduledAsync;

    public event AsyncEventHandler<OnErrorArgs>? OnBotScheduledErrorAsync;

    public async Task StartAsync(CancellationToken cancellationToken = default) =>
        await _botScheduledProcessor.StartProcessingAsync(cancellationToken);

    public async Task StopAsync(CancellationToken cancellationToken = default) =>
        await _botScheduledProcessor.StopProcessingAsync(cancellationToken);

    private void RegisterMessageCallback() =>
        _botScheduledProcessor.ProcessMessageAsync += async args =>
        {
            LastMessageReceivedAt = DateTime.Now;
            var payload = MemoryPackSerializer.Deserialize<BotScheduledPayload>(args.Message.Body) ?? throw new InvalidOperationException("Could not deserialize the message body.");
            await OnBotScheduledAsync.InvokeAsync(payload, args.CancellationToken);
        };

    private void RegisterErrorCallback() =>
        _botScheduledProcessor.ProcessErrorAsync += async args =>
        {
            OnErrorArgs onErrorArgs = new() { Exception = args.Exception };
            await OnBotScheduledErrorAsync.InvokeAsync(onErrorArgs, args.CancellationToken);
        };
}