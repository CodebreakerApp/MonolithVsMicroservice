using CodeBreaker.Services.Bot.Messaging.Transfer.Payloads;

namespace CodeBreaker.Services.Bot.Messaging.Services;
public interface IBotScheduler
{
    Task ScheduleBotAsync(BotScheduledPayload payload, CancellationToken cancellationToken);
}