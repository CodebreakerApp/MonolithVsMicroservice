using MemoryPack;

namespace CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

[MemoryPackable]
public partial class GameCreatedPayload
{
    public required int Id { get; init; }

    public required string Type { get; init; }

    public required string Username { get; init; }

    public required IReadOnlyList<Field> Code { get; init; }

    public required DateTime Start { get; init; }
}
