using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.GameTypes;
using CodeBreaker.Services.Games.GameLogic.Extensions;

namespace CodeBreaker.Services.Games.GameLogic.Visitors;

internal record class GameFactoryParameters(string Username);

internal class GameFactoryVisitor : IGameTypeVisitor<Game>
{
    public GameFactoryVisitor(GameFactoryParameters parameters) =>
        Parameters = parameters;

    public GameFactoryParameters Parameters { get; protected init; }

    public Game Visit(GameType6x4 gameType) =>
        new Game()
        {
            Type = gameType,
            Username = Parameters.Username,
            Start = DateTime.Now,
            Code = gameType.GetRandomFields().ToArray()
        };

    public Game Visit(GameType8x5 gameType) =>
        new Game()
        {
            Type = gameType,
            Username = Parameters.Username,
            Start = DateTime.Now,
            Code = gameType.GetRandomFields().ToArray()
        };
}


internal static class GameTypeVisitorExtensions
{
    public static Game CreateGame(this GameType gameType, GameFactoryParameters parameters) =>
        gameType.Accept(new GameFactoryVisitor(parameters));
}
