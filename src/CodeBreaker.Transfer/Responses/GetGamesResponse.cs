namespace CodeBreaker.Transfer.Responses;

public class GetGamesResponse
{
    public required IAsyncEnumerable<Game> Games { get; set; }
}
