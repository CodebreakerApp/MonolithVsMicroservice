namespace CodeBreaker.Backend.Data.Models.GameTypes;

public interface IGameTypeVisitor<out TResult>
{
    TResult Visit(GameType6x4 gameType);

    TResult Visit(GameType8x5 gameType);
}

public interface IGameTypeVisitor : IGameTypeVisitor<Empty> { }

public interface IGameTypeVisitable
{
    TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor);
}