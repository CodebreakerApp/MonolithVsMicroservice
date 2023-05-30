using System.Text.Json.Serialization;

namespace CodeBreaker.Backend.Data.Models.Bots;

[JsonDerivedType(typeof(SimpleBot), "simple")]
public abstract record class Bot : IBotVisitable
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public Game? Game { get; set; }

    public int ThinkTime { get; set; }

    public abstract TResult Accept<TResult>(IBotVisitor<TResult> visitor);
}
