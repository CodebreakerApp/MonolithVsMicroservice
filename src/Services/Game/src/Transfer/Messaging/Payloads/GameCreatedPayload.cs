using MemoryPack;

namespace CodeBreaker.Services.Games.Transfer.Messaging.Payloads;

[MemoryPackable]
public class GameCreatedPayload
{
    public required int Id { get; init; }

    public required string Type { get; init; }

    public required string Username { get; init; }

    public required DateTime Start { get; init; }
}
