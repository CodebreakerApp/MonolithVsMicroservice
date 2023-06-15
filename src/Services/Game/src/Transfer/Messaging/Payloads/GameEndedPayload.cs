using MemoryPack;

namespace CodeBreaker.Services.Games.Transfer.Messaging.Payloads;

[MemoryPackable]
public class GameEndedPayload
{
    public required int Id { get; init; }

    public required DateTime End { get; init; }

    public required bool Won { get; init; }

    public required bool Cancelled { get; init; }
}
