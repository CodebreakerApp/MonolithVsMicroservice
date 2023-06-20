using CodeBreaker.Services.Bot.Runner.Models.GameTypes;

namespace CodeBreaker.Services.Bot.Runner.Models;

public class Game
{
    public Guid Id { get; set; }

    public GameType Type { get; set; }

    public required string Username { get; set; }

    public required DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    public required GameState State { get; set; }
}