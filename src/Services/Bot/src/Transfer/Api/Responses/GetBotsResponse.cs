namespace CodeBreaker.Services.Bot.Transfer.Api.Responses;

public class GetBotsResponse
{
    public required IAsyncEnumerable<Bot> Bots { get; init; }
}
