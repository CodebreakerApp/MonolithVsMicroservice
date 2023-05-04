using CodeBreaker.Backend.Data.Models.GameTypes;

namespace CodeBreaker.Backend.Data.Repositories;
public interface IGameTypeRepository
{
    IEnumerable<string> GameTypeNames { get; }
    IEnumerable<GameType> GameTypes { get; }

    GameType GetGameType(string gameTypeName);
    IEnumerable<string> GetGameTypeNames();
    IEnumerable<GameType> GetGameTypes();
}