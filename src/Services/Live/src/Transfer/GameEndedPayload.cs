namespace CodeBreaker.Services.Live.Transfer;

public class GameEndedPayload
{
    public required Guid Id { get; init; }

    public required DateTime End { get; init; }

    public required string State { get; init; }
}