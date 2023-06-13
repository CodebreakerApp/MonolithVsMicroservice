namespace CodeBreaker.Services.Games.Transfer.Api.Requests;

public class CreateGameRequest
{
    public required string GameType { get; set; }

    public required string Username { get; set; }
}
