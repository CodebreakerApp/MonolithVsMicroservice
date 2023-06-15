namespace CodeBreaker.Services.Report.Transfer.Api.Responses;

public class GetGamesResponse
{
    public required IAsyncEnumerable<Game> Games { get; init; }
}
