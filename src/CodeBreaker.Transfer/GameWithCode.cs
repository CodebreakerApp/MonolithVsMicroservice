namespace CodeBreaker.Transfer;

public class GameWithCode : Game
{
    public required IReadOnlyList<Field> Code { get; set; }
}
