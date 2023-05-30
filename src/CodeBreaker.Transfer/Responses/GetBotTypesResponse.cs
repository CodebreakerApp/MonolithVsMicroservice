namespace CodeBreaker.Transfer.Responses;

public class GetBotTypesResponse
{
    public required IReadOnlyList<string> BotTypeNames { get; set; }
}
