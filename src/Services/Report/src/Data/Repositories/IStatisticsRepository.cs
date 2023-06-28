using CodeBreaker.Services.Report.Data.Models;

namespace CodeBreaker.Services.Report.Data.Repositories;
public interface IStatisticsRepository : IDisposable
{
    Task<Statistics> GetStatisticsAsync(GetStatisticsArgs args, CancellationToken cancellationToken = default);
}