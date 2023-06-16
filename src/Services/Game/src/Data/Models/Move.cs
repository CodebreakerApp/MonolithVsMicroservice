using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.Data.Models;

public class Move
{
    public Guid Id { get; set; }

    public required IReadOnlyList<Field> Fields { get; init; }

    public required IReadOnlyList<KeyPeg> KeyPegs { get; set; }

    public required DateTime CreatedAt { get; init; }
}