namespace CodeBreaker.Services.Report.Transfer.Api;

public class Move
{
    public required IReadOnlyList<Field> Fields { get; set; }

    public IReadOnlyList<string>? KeyPegs { get; set; }

    public required DateTime CreatedAt { get; set; }
}