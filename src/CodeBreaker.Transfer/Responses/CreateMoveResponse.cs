namespace CodeBreaker.Transfer.Responses;

public class CreateMoveResponse
{
    public required IEnumerable<string> KeyPegs { get; set; }

    public bool GameEnded { get; set; }

    public bool GameWon { get; set; }
}
