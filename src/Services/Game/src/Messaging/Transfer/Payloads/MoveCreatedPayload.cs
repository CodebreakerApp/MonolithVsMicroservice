using MemoryPack;

namespace CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

[MemoryPackable]
public partial class MoveCreatedPayload
{
    public required int GameId { get; init; }

    public required int MoveId { get; init; }

    public required IReadOnlyList<Field> Fields { get; init; }

    public required IReadOnlyList<string> KeyPegs { get; init; }

    public required int Position { get; init; }
}
