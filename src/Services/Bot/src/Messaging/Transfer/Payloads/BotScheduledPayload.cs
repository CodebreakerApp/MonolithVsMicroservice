using MemoryPack;

namespace CodeBreaker.Services.Bot.Messaging.Transfer.Payloads;

[MemoryPackable]
public partial class BotScheduledPayload
{
    public required Guid Id { get; init; }

    public required string Type { get; init; }

    public required string GameType { get; init; }

    public required int ThinkTime { get; init; }

    public required string Username { get; init; }
}
