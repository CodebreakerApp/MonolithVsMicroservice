using CodeBreaker.Services.Bot.Runner.Models.GameTypes;

namespace CodeBreaker.Services.Bot.Runner.Services;
internal interface IGameTypeService
{
    Task<GameType> FetchGameTypeAsync(string gameTypeName, CancellationToken cancellationToken);
}