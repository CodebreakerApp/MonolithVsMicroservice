using Azure.Messaging.ServiceBus;
using CodeBreaker.Services.Games.Transfer.Messaging.Payloads;
using MemoryPack;

namespace CodeBreaker.Services.Games.Messaging.Services;

/// <remarks>
/// Recommended lifetime: Singleton<br/>
/// Threadsafe: Yes
/// </remarks>
public class MessagePublisher(ServiceBusClient serviceBusClient) : IMessagePublisher
{
    private readonly ServiceBusSender _gameCreatedSender = serviceBusClient.CreateSender(MessagingTopics.Game_Created);

    private readonly ServiceBusSender _moveCreatedSender = serviceBusClient.CreateSender(MessagingTopics.Game_Move_Created);

    private readonly ServiceBusSender _gameEndedSender = serviceBusClient.CreateSender(MessagingTopics.Game_Ended);

    public async Task PublishGameCreatedAsync(GameCreatedPayload payload, CancellationToken cancellationToken) =>
        await Publish(_gameCreatedSender, payload, cancellationToken);

    public async Task PublishMoveCreatedAsync(MoveCreatedPayload payload, CancellationToken cancellationToken) =>
        await Publish(_moveCreatedSender, payload, cancellationToken);

    public async Task PublishGameEndedAsync(GameEndedPayload payload, CancellationToken cancellationToken) =>
        await Publish(_gameEndedSender, payload, cancellationToken);

    private Task Publish<TPayload>(ServiceBusSender sender, TPayload payload, CancellationToken cancellationToken)
    {
        var message = new ServiceBusMessage(MemoryPackSerializer.Serialize(payload))
        {
            ContentType = "application/msgpack"
        };
        return sender.SendMessageAsync(message, cancellationToken);
    }
}