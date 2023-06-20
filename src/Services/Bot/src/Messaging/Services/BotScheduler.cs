using Azure.Messaging.ServiceBus;
using CodeBreaker.Services.Bot.Messaging.Transfer.Payloads;
using MemoryPack;

namespace CodeBreaker.Services.Bot.Messaging.Services;

public class BotScheduler(ServiceBusClient serviceBusClient) : IBotScheduler
{
    private readonly ServiceBusSender _botScheduler = serviceBusClient.CreateSender(MessageQueueNames.ScheduledBBots);

    public async Task ScheduleBotAsync(BotScheduledPayload payload, CancellationToken cancellationToken) =>
        await Publish(_botScheduler, payload, cancellationToken);

    private Task Publish<TPayload>(ServiceBusSender sender, TPayload payload, CancellationToken cancellationToken)
    {
        var message = new ServiceBusMessage(MemoryPackSerializer.Serialize(payload))
        {
            ContentType = "application/msgpack"
        };
        return sender.SendMessageAsync(message, cancellationToken);
    }
}
