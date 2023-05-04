using CodeBreaker.Backend.Data.Models.GameTypes;

namespace CodeBreaker.Backend.Data.Models.KeyPegs;

public interface IKeyPegVisitor<out TResult>
{
    TResult Visit(BlackKeyPeg keyPeg);

    TResult Visit(WhiteKeyPeg keyPeg);
}

public interface IKeyPegVisitor : IGameTypeVisitor<Empty> { }

public interface IKeyPegVisitable
{
    TResult Accept<TResult>(IKeyPegVisitor<TResult> visitor);
}