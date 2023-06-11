using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.Data.Models;

public class Move
{
    public int Id { get; set; }

    public required IReadOnlyList<Field> Fields { get; init; }

    public IReadOnlyList<KeyPeg>? KeyPegs { get; set; }
}