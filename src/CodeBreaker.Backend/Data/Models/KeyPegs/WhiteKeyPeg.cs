namespace CodeBreaker.Backend.Data.Models.KeyPegs;

public record class WhiteKeyPeg : KeyPeg
{
    public override TResult Accept<TResult>(IKeyPegVisitor<TResult> visitor) =>
        visitor.Visit(this);
}