namespace CodeBreaker.Transfer.Responses;

public class GetReportGamesResponse
{
    public required IAsyncEnumerable<GameWithCode> Games { get; init; }
}
