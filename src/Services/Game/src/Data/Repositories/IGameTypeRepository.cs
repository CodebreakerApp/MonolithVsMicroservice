using CodeBreaker.Services.Games.Data.Models.GameTypes;

namespace CodeBreaker.Services.Games.Data.Repositories;

public interface IGameTypeRepository
{
    IReadOnlyList<string> GameTypeNames { get; }
    IReadOnlyList<GameType> GameTypes { get; }

    GameType GetGameType(string gameTypeName);
    IReadOnlyList<string> GetGameTypeNames();
    IReadOnlyList<GameType> GetGameTypes();
}