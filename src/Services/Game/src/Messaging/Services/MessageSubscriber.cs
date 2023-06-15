using Azure.Messaging.ServiceBus;
using CodeBreaker.Services.Games.Messaging.Args;
using CodeBreaker.Services.Games.Transfer.Messaging.Payloads;
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
                .RegisterMessageCallback(this, OnGameCreatedAsync)
                .RegisterErrorCallback(OnGameCreatedErrorAsync),
            serviceBusClient
                .CreateProcessor(MessagingTopics.Game_Move_Created, subscriptionName, _options)
                .RegisterMessageCallback(this, OnMoveCreatedAsync)
                .RegisterErrorCallback(OnMoveCreatedErrorAsync),
            serviceBusClient
                .CreateProcessor(MessagingTopics.Game_Ended, subscriptionName, _options)
                .RegisterMessageCallback(this, OnGameEndedAsync)
                .RegisterErrorCallback(OnGameEndedErrorAsync)
        };
    }

    public DateTime? LastMessageReceivedAt { get; internal set; }

    public TimeSpan? NoMessageDuration => DateTime.Now - LastMessageReceivedAt;


    public event Func<GameCreatedPayload, Task>? OnGameCreatedAsync;

    public event Func<OnErrorArgs, Task>? OnGameCreatedErrorAsync;


    public event Func<MoveCreatedPayload, Task>? OnMoveCreatedAsync;

    public event Func<OnErrorArgs, Task>? OnMoveCreatedErrorAsync;


    public event Func<GameEndedPayload, Task>? OnGameEndedAsync;

    public event Func<OnErrorArgs, Task>? OnGameEndedErrorAsync;

    public async Task StartAsync(CancellationToken cancellationToken = default) =>
        await Task.WhenAll(_processors.Select(p => p.StartProcessingAsync(cancellationToken)));

    public async Task StopAsync(CancellationToken cancellationToken = default) =>
        await Task.WhenAll(_processors.Select(p => p.StopProcessingAsync(cancellationToken)));
}

file static class MessageSubscriberExtensions
{
    public static ServiceBusProcessor RegisterMessageCallback<TPayload>(this ServiceBusProcessor processor, MessageSubscriber messageSubscriber, Func<TPayload, Task>? resultEventToInvoke)
    {
        messageSubscriber.LastMessageReceivedAt = DateTime.Now;
        processor.ProcessMessageAsync += args => resultEventToInvoke?.InvokeAsync(args);
        return processor;
    }

    public static ServiceBusProcessor RegisterErrorCallback(this ServiceBusProcessor processor, Func<OnErrorArgs, Task>? errorEventToInvoke)
    {
        processor.ProcessErrorAsync += args => errorEventToInvoke?.InvokeAsync(args);
        return processor;
    }

    public static async Task InvokeAsync<TPayload>(this Func<TPayload, Task> eventToInvoke, ProcessMessageEventArgs args)
    {
        var payload = MemoryPackSerializer.Deserialize<TPayload>(args.Message.Body) ?? throw new InvalidOperationException("Could not deserialize the message body.");
        await eventToInvoke(payload);
    }

    public static async Task InvokeAsync(this Func<OnErrorArgs, Task> eventToInvoke, ProcessErrorEventArgs args)
    {
        OnErrorArgs onErrorArgs = new() { Exception = args.Exception };
        await eventToInvoke(onErrorArgs);
    }
}