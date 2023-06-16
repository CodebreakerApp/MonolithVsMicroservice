using CodeBreaker.Services.Report.Data.Models.Fields;

namespace CodeBreaker.Services.Report.Data.Models;

public class Game
{
    public Guid Id { get; set; }

    public required string Type { get; set; }

    public required string Username { get; set; }

    public required DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public required IReadOnlyList<Field> Code { get; set; }

    public List<Move> Moves { get; set; } = new List<Move>();

    public bool Won { get; set; }

    public bool Cancelled { get; set; }
}