namespace CodeBreaker.Backend.Data.Models.Bots;

public record class SimpleBot : Bot
{
    public override TResult Accept<TResult>(IBotVisitor<TResult> visitor) =>
        visitor.Visit(this);
}
