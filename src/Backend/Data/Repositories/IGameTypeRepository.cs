using CodeBreaker.Backend.Data.Models.GameTypes;

namespace CodeBreaker.Backend.Data.Repositories;
public interface IGameTypeRepository
{
    IReadOnlyList<string> GameTypeNames { get; }
    IReadOnlyList<GameType> GameTypes { get; }

    GameType GetGameType(string gameTypeName);
    IReadOnlyList<string> GetGameTypeNames();
    IReadOnlyList<GameType> GetGameTypes();
}