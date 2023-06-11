using CodeBreaker.Backend.Visitors;

namespace CodeBreaker.Backend.Data.Models.GameTypes;

public static class GameTypeVisitorExtensions
{
    public static string GetName(this GameType gameType) =>
        gameType.Accept(new GameTypeNameVisitor());

    public static Game CreateGame(this GameType gameType, GameFactoryParameters parameters) =>
        gameType.Accept(new GameFactoryVisitor(parameters));
}
