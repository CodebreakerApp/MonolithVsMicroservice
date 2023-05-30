using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Visitors.Extensions;
using System.Collections.Immutable;

namespace CodeBreaker.Backend.Visitors;


public class GameTypeNameVisitor : IGameTypeVisitor<string>
{
    public string Visit(GameType6x4 gameType) => "6x4";

    public string Visit(GameType8x5 gameType) => "8x5";
}


public record class GameFactoryParameters(string Username);

public class GameFactoryVisitor : IGameTypeVisitor<Game>
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
            Code = gameType.GetRandomFields().ToImmutableArray()
        };

    public Game Visit(GameType8x5 gameType) =>
        new Game()
        {
            Type = gameType,
            Username = Parameters.Username,
            Start = DateTime.Now,
            Code = gameType.GetRandomFields().ToImmutableArray()
        };
}