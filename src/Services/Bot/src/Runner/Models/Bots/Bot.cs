namespace CodeBreaker.Services.Bot.Runner.Models.Bots;

public abstract class Bot : IBotVisitable
{
    public required Guid Id { get; set; }

    public required Guid GameId { get; set; }

    public required Game Game { get; set; }

    public required int ThinkTime { get; set; }

    public abstract TResult Accept<TResult>(IBotVisitor<TResult> visitor);
}
