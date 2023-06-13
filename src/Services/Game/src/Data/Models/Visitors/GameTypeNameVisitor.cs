using CodeBreaker.Services.Games.Data.Models.GameTypes;

namespace CodeBreaker.Services.Games.Data.Models.Visitors;

public class GameTypeNameVisitor : IGameTypeVisitor<string>
{
    public string Visit(GameType6x4 _) => "6x4";

    public string Visit(GameType8x5 _) => "8x5";
}

public static class GameTypeNameVisitorExtensions
{
    public static string GetName(this GameType gameType) =>
        gameType.Accept(new GameTypeNameVisitor());
}