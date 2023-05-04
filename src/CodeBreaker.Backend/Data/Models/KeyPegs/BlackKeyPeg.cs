namespace CodeBreaker.Backend.Data.Models.KeyPegs;

public record class BlackKeyPeg : KeyPeg
{
    public override TResult Accept<TResult>(IKeyPegVisitor<TResult> visitor) =>
        visitor.Visit(this);
}