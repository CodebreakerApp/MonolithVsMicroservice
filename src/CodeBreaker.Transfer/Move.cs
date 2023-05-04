namespace CodeBreaker.Transfer;

public class Move
{
    public required List<Field> Fields { get; set; }

    public List<string>? KeyPegs { get; set; }
}