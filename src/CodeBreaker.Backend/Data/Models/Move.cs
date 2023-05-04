using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.KeyPegs;

namespace CodeBreaker.Backend.Data.Models;

public class Move
{
    public required List<Field> Fields { get; init; }

    public List<KeyPeg>? KeyPegs { get; set; }
}