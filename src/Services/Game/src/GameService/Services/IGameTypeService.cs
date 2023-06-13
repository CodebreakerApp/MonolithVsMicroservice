using CodeBreaker.Services.Games.Data.Models.GameTypes;

namespace CodeBreaker.Services.Games.Services;

internal interface IGameTypeService
{
    IReadOnlyList<GameType> GetGameTypes();

    GameType GetGameType(string name);
}