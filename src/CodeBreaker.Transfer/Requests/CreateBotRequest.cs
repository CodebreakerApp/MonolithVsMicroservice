namespace CodeBreaker.Transfer.Requests;

public class CreateBotRequest
{
    public required string BotName { get; set; }

    public required string GameType { get; set; }

    public int ThinkTime { get; set; } = 0;
}
