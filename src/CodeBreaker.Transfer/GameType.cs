namespace CodeBreaker.Transfer;

public class GameType
{
    public required string Name { get; set; }

    public required int Holes { get; set; }

    public required int MaxMoves { get; set; }

    public required IEnumerable<Field> PossibleFields { get; set; }
}
