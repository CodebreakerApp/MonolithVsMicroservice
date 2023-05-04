using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Data.Repositories;

namespace CodeBreaker.Backend.Services;

public class GameTypeService(IGameTypeRepository gameTypeRepository) : IGameTypeService
{
    public IEnumerable<GameType> GetGameTypes() => gameTypeRepository.GetGameTypes();

    public GameType GetGameType(string name) => gameTypeRepository.GetGameType(name);
}