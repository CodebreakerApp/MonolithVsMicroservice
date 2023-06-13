namespace CodeBreaker.Services.Games.Transfer.Api.Responses;

public class GetGameTypesResponse
{
    public required IReadOnlyList<GameType> GameTypes { get; set; }
}
