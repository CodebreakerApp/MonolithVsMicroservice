using CodeBreaker.Services.Games.Data.Models.Fields;
using CodeBreaker.Services.Games.Data.Models.GameTypes;

namespace CodeBreaker.Services.Games.Data.Models;

public class Game
{
    public Guid Id { get; set; }

    public required GameType Type { get; set; }

    public required string Username { get; set; }

    public required DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public required IReadOnlyList<Field> Code { get; set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    public required GameState State { get; set; }
}