using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

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
            Code = gameType.GetRandomFields()
        };

    public Game Visit(GameType8x5 gameType) =>
        new Game()
        {
            Type = gameType,
            Username = Parameters.Username,
            Start = DateTime.Now,
            Code = gameType.GetRandomFields()
        };
}

file static class FieldExtensions
{
    public static List<Field> GetRandomFields(this GameType gameType) =>
        GetRandom(gameType.PossibleFields, gameType.Holes);

    public static List<Field> GetRandom(this List<Field> possibleFields, int count) =>
        Enumerable.Range(0, count)
            .Select(_ => possibleFields.GetRandom())
            .ToList();

    public static Field GetRandom(this List<Field> possibleFields)
    {
        int max = possibleFields.Count;
        int index = Random.Shared.Next(max);
        return possibleFields[index];
    }
}