using MemoryPack;

namespace CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

[MemoryPackable]
public partial class GameEndedPayload
{
    public required Guid Id { get; init; }

    public required DateTime End { get; init; }

    public required bool Won { get; init; }

    public required bool Cancelled { get; init; }
}
