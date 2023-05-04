using System.Text.Json.Serialization;

namespace CodeBreaker.Backend.Data.Models.KeyPegs;

[JsonDerivedType(typeof(KeyPeg))]
[JsonDerivedType(typeof(WhiteKeyPeg))]
[JsonDerivedType(typeof(BlackKeyPeg))]
public abstract record class KeyPeg : IKeyPegVisitable
{
    public abstract TResult Accept<TResult>(IKeyPegVisitor<TResult> visitor);
}