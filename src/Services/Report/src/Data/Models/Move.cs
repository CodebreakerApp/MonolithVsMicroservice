using CodeBreaker.Services.Report.Data.Models.Fields;
using CodeBreaker.Services.Report.Data.Models.KeyPegs;

namespace CodeBreaker.Services.Report.Data.Models;

public class Move
{
    public Guid Id { get; set; }

    public Guid GameId { get; set; }

    public required IReadOnlyList<Field> Fields { get; init; }

    public IReadOnlyList<KeyPeg>? KeyPegs { get; set; }

    public required DateTime CreatedAt { get; set; }
}