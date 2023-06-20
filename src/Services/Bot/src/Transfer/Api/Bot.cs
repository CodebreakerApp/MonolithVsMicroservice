namespace CodeBreaker.Services.Bot.Transfer.Api;

public class Bot
{
    public required Guid Id { get; init; }

    public Guid? GameId { get; init; }
    
    public required string GameType { get; init; }

    public required int ThinkTime { get; init; }

    public required string State { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required DateTime? EndedAt { get; init; }
}
