namespace CodeBreaker.Transfer;

public class Game
{
    public required Guid Id { get; set; }

    public required string Type { get; set; }

    public required string Username { get; set; }

    public required DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public List<Move> Moves { get; set; } = new List<Move>();
}