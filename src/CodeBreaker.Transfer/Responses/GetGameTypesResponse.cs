namespace CodeBreaker.Transfer.Responses;

public class GetGameTypesResponse
{
    public required IReadOnlyList<GameType> GameTypes { get; set; }
}
