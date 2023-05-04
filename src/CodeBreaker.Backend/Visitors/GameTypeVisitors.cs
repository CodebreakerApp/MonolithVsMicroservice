using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;

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
            Id = Guid.NewGuid(),
            Type = gameType,
            Username = Parameters.Username,
            Start = DateTime.Now,
            Code = new List<Field>()
            {
                new ColorField(FieldColor.Red),
                new ColorField(FieldColor.Green),
                new ColorField(FieldColor.Blue),
                new ColorField(FieldColor.Yellow)
            }
        };

    public Game Visit(GameType8x5 gameType) =>
        new Game()
        {
            Id = Guid.NewGuid(),
            Type = gameType,
            Username = Parameters.Username,
            Start = DateTime.Now,
            Code = new List<Field>()
            {
                new ColorField(FieldColor.Red),
                new ColorField(FieldColor.Green),
                new ColorField(FieldColor.Blue),
                new ColorField(FieldColor.Yellow),
                new ColorField(FieldColor.Yellow)
            }
        };
}