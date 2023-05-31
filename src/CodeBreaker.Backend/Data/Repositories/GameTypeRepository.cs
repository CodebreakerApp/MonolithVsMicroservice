using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Common.Exceptions;
using System.Collections.Frozen;

namespace CodeBreaker.Backend.Data.Repositories;

public class GameTypeRepository : IGameTypeRepository
{
    private static readonly FrozenDictionary<string, GameType> _mapping = new GameType[]
        {
            new GameType6x4(),
            new GameType8x5(),
        }.ToFrozenDictionary(x => x.GetName());

    public IReadOnlyList<GameType> GameTypes =>
        _mapping.Values;

    public IReadOnlyList<string> GameTypeNames =>
        _mapping.Keys;

    public IReadOnlyList<GameType> GetGameTypes() =>
        GameTypes;

    public GameType GetGameType(string gameTypeName) =>
        GetGameTypeOrDefault(gameTypeName) ?? throw new NotFoundException();

    public GameType? GetGameTypeOrDefault(string gameTypeName) =>
        _mapping.GetValueOrDefault(gameTypeName);

    public string GetGameTypeName(GameType gameType) =>
        gameType.GetName();

    public IReadOnlyList<string> GetGameTypeNames() =>
        GameTypeNames;
}