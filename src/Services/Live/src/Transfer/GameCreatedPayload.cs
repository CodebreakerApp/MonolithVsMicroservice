namespace CodeBreaker.Services.Live.Transfer;

public class GameCreatedPayload
{
    public required Guid Id { get; init; }

    public required string Type { get; init; }

    public required string Username { get; init; }

    public required IReadOnlyList<Field> Code { get; init; }

    public required DateTime Start { get; init; }
}