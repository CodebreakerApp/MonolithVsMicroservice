namespace CodeBreaker.Services.Bot.Transfer.Api.Requests;
public class CreateBotRequest
{
    public required string Name { get; init; }

    public required string BotType { get; init; }

    public required string GameType { get; init; }

    public int ThinkTime { get; init; } = 0;
}
