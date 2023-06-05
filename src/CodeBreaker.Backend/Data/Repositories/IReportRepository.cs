using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.Data.Repositories;
public interface IReportRepository
{
    Task<Game> GetGameAsync(GetGameArgs args, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetGamesAsync(GetGamesArgs args, CancellationToken cancellationToken = default);
    Task<Statistics> GetStatisticsAsync(GetStatisticsArgs args, CancellationToken cancellationToken = default);
}