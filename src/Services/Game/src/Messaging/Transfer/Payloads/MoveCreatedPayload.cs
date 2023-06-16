using MemoryPack;

namespace CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

[MemoryPackable]
public partial class MoveCreatedPayload
{
    public required Guid GameId { get; init; }

    public required Guid MoveId { get; init; }

    public required IReadOnlyList<Field> Fields { get; init; }

    public required IReadOnlyList<string> KeyPegs { get; init; }
}
