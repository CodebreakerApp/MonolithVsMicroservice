namespace CodeBreaker.Services.Bot.Runner.Models.Bots;

public class SimpleBot : Bot
{
    public override TResult Accept<TResult>(IBotVisitor<TResult> visitor) =>
        visitor.Visit(this);
}
