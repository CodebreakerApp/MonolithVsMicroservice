using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;
using System.Collections.Immutable;

namespace CodeBreaker.Backend.Data.Models;

public class Game
{
    public int Id { get; set; }

    public required GameType Type { get; set; }

    public required string Username { get; set; }

    public required DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public required ImmutableArray<Field> Code { get; set; }

    public List<Move> Moves { get; set; } = new List<Move>();
}