namespace CodeBreaker.Transfer;

public class Move
{
    public required IReadOnlyList<Field> Fields { get; set; }

    public IReadOnlyList<string>? KeyPegs { get; set; }
}