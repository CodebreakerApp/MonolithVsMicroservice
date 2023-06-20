namespace CodeBreaker.Services.Bot.Transfer.Api.Requests;

public class GetBotsRequest
{
    public required DateTime From { get; init; }

    public required DateTime To { get; init; }

    public required int MaxCount { get; init; }
}
