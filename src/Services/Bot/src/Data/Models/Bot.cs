namespace CodeBreaker.Services.Bot.Data.Models;

public abstract class Bot : IBotVisitable
{
    public Guid Id { get; set; }

    public Guid? GameId { get; set; }

    public required string Name { get; init; }

    public required string Type { get; init; }

    public required string GameType { get; init; }

    public required int ThinkTime { get; init; }

    public required BotState State { get; set; }

    public required DateTime CreatedAt { get; init; }

    public DateTime? EndedAt { get; set; }

    public abstract TResult Accept<TResult>(IBotVisitor<TResult> visitor);
}
