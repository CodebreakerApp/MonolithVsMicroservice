using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.Services;
public interface IReportService
{
    Task<Game> GetGameAsync(GetGameArgs args, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetGamesAsync(GetGamesArgs args, CancellationToken cancellationToken = default);
    Task<Statistics> GetStatisticsAsync(GetStatisticsArgs args, CancellationToken cancellationToken = default);
}