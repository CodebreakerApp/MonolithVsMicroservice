using MemoryPack;

namespace CodeBreaker.Services.Games.Transfer.Messaging.Payloads;

[MemoryPackable]
public class MoveCreatedPayload
{
    public required int GameId { get; init; }

    public required int MoveId { get; init; }

    public required IReadOnlyList<Field> Fields { get; init; }

    public required IReadOnlyList<string> KeyPegs { get; init; }
}
