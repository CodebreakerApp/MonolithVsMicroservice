using CodeBreaker.Backend.Data.Models.GameTypes;

namespace CodeBreaker.Backend.Services;
public interface IGameTypeService
{
    IReadOnlyList<GameType> GetGameTypes();

    GameType GetGameType(string name);
}