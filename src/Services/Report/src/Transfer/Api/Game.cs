﻿namespace CodeBreaker.Services.Report.Transfer.Api;

public class Game
{
    public required Guid Id { get; set; }

    public required string Type { get; set; }

    public required string Username { get; set; }

    public required IReadOnlyList<Field> Code { get; set; }

    public required DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public IReadOnlyList<Move> Moves { get; set; } = Array.Empty<Move>();
}