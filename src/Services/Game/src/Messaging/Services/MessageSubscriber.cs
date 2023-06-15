using Azure.Messaging.ServiceBus;
using CodeBreaker.Services.Games.Messaging.Args;
using CodeBreaker.Services.Games.Messaging.AsyncEvent;
using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;
using MemoryPack;

namespace CodeBreaker.Services.Games.Messaging.Services;

/// <remarks>
/// Recommended lifetime: Singleton<br/>
/// Threadsafe: Yes
/// </remarks>
public class MessageSubscriber : IMessageSubscriber
{
    private readonly IEnumerable<ServiceBusProcessor> _processors;
    private readonly static ServiceBusProcessorOptions _options = new() { AutoCompleteMessages = true };

    public MessageSubscriber(ServiceBusClient serviceBusClient, string subscriptionName)
    {
        if (string.IsNullOrWhiteSpace(subscriptionName))
            throw new ArgumentException("SubscriptionName is required", nameof(subscriptionName));

        _processors = new[]
        {
            serviceBusClient
                .CreateProcessor(MessagingTopics.Game_Created, subscriptionName, _options)
                .RegisterMessageCallback(this, () => OnGameCreatedAsync)
                .RegisterErrorCallback(() => OnGameCreatedErrorAsync),
            serviceBusClient
                .CreateProcessor(MessagingTopics.Game_Move_Created, subscriptionName, _options)
                .RegisterMessageCallback(this, () => OnMoveCreatedAsync)
                .RegisterErrorCallback(() => OnMoveCreatedErrorAsync),
            serviceBusClient
                .CreateProcessor(MessagingTopics.Game_Ended, subscriptionName, _options)
                .RegisterMessageCallback(this, () => OnGameEndedAsync)
                .RegisterErrorCallback(() => OnGameEndedErrorAsync)
        };
    }

    public DateTime? LastMessageReceivedAt { get; internal set; }

    public TimeSpan? NoMessageDuration => DateTime.Now - LastMessageReceivedAt;


    public event AsyncEventHandler<GameCreatedPayload>? OnGameCreatedAsync;

    public event AsyncEventHandler<OnErrorArgs>? OnGameCreatedErrorAsync;


    public event AsyncEventHandler<MoveCreatedPayload>? OnMoveCreatedAsync;

    public event AsyncEventHandler<OnErrorArgs>? OnMoveCreatedErrorAsync;


    public event AsyncEventHandler<GameEndedPayload>? OnGameEndedAsync;

    public event AsyncEventHandler<OnErrorArgs>? OnGameEndedErrorAsync;

    public async Task StartAsync(CancellationToken cancellationToken = default) =>
        await Task.WhenAll(_processors.Select(p => p.StartProcessingAsync(cancellationToken)));

    public async Task StopAsync(CancellationToken cancellationToken = default) =>
        await Task.WhenAll(_processors.Select(p => p.StopProcessingAsync(cancellationToken)));
}

file static class MessageSubscriberExtensions
{
    public static ServiceBusProcessor RegisterMessageCallback<TPayload>(this ServiceBusProcessor processor, MessageSubscriber messageSubscriber, Func<AsyncEventHandler<TPayload>?> eventResolver)
    {
        processor.ProcessMessageAsync += async args =>
        {
            messageSubscriber.LastMessageReceivedAt = DateTime.Now;
            var payload = MemoryPackSerializer.Deserialize<TPayload>(args.Message.Body) ?? throw new InvalidOperationException("Could not deserialize the message body.");
            await eventResolver.InvokeAsync(payload, args.CancellationToken);
        };
        return processor;
    }

    public static ServiceBusProcessor RegisterErrorCallback(this ServiceBusProcessor processor, Func<AsyncEventHandler<OnErrorArgs>?> eventResolver)
    {
        processor.ProcessErrorAsync += async args =>
        {
            OnErrorArgs onErrorArgs = new() { Exception = args.Exception };
            await eventResolver.InvokeAsync(onErrorArgs, args.CancellationToken);
        };
        return processor;
    }
}