using CodeBreaker.Services.Games.Data.Models.GameTypes;
using CodeBreaker.Services.Games.Data.Repositories;

namespace CodeBreaker.Services.Games.Services;

internal class GameTypeService(IGameTypeRepository gameTypeRepository) : IGameTypeService
{
    public IReadOnlyList<GameType> GetGameTypes() => gameTypeRepository.GetGameTypes();

    public GameType GetGameType(string name) => gameTypeRepository.GetGameType(name);
}