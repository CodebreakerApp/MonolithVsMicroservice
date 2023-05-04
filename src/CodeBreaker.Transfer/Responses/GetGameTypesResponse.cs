namespace CodeBreaker.Transfer.Responses;

public class GetGameTypesResponse
{
    public required IEnumerable<GameType> GameTypes { get; set; }
}
