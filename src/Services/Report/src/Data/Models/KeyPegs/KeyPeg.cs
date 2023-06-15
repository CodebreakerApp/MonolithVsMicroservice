namespace CodeBreaker.Services.Report.Data.Models.KeyPegs;

public class KeyPeg
{
    public KeyPeg(KeyPegColor color)
    {
        Color = color;
    }

    public int Id { get; set; }

    public KeyPegColor Color { get; init; }

    internal int Position { get; set; }

    public static implicit operator KeyPegColor(KeyPeg keyPeg) => keyPeg.Color;
    public static implicit operator KeyPeg(KeyPegColor color) => new (color);
}
