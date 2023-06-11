namespace CodeBreaker.Transfer.Requests;

public class GetStatisticsRequest
{
    public required DateTime From { get; init; }

    public required DateTime To { get; init; }

    public string? GameType { get; init; }
}
