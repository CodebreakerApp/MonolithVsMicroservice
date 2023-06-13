namespace CodeBreaker.Services.Games.Transfer.Api;

public class GameWithCode : Game
{
    public required IReadOnlyList<Field> Code { get; set; }
}
