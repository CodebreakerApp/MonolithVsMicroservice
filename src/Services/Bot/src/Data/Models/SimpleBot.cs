namespace CodeBreaker.Services.Bot.Data.Models;

public class SimpleBot : Bot
{
    public override TResult Accept<TResult>(IBotVisitor<TResult> visitor) =>
        visitor.Visit(this);
}
