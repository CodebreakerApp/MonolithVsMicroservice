namespace CodeBreaker.Services.Games.Transfer.Api.Responses;

public class GetGamesResponse
{
    public required IAsyncEnumerable<Game> Games { get; set; }
}
