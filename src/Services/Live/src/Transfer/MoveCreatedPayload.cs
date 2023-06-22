namespace CodeBreaker.Services.Live.Transfer;

public class MoveCreatedPayload
{
    public required Guid GameId { get; init; }

    public required Guid MoveId { get; init; }

    public required IReadOnlyList<Field> Fields { get; init; }

    public required IReadOnlyList<string> KeyPegs { get; init; }

    public required DateTime CreatedAt { get; init; }
}