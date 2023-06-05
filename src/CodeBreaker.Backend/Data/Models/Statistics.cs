namespace CodeBreaker.Backend.Data.Models;

public class Statistics
{
    public required int TotalGames { get; init; }

    public required int WonGames { get; init; }

    public required int CancelledGames { get; init; }

    public required int MinMoveCount { get; init; }

    public required double AvgMoveCount { get; init; }

    public required int MaxMoveCount { get; init; }

    public required TimeSpan MinGameDuration { get; init; }

    public required TimeSpan AvgGameDuration { get; init; }

    public required TimeSpan MaxGameDuration { get; init; }
}
