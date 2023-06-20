namespace CodeBreaker.Services.Bot.Data.Models;

public interface IBotVisitor<TResult>
{
    TResult Visit(SimpleBot bot);
}

public interface IBotVisitor : IBotVisitor<Empty> { }

public interface IBotVisitable
{
    TResult Accept<TResult>(IBotVisitor<TResult> visitor);
}