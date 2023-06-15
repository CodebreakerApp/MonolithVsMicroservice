using CodeBreaker.Services.Report.Data.Models;

namespace CodeBreaker.Services.Report.WebApi.Services;
internal interface IGameService
{
    Task<Game> GetGameAsync(GetGameArgs args, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetGamesAsync(GetGamesArgs args, CancellationToken cancellationToken = default);
}