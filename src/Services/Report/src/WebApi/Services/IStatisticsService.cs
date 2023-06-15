using CodeBreaker.Services.Report.Data.Models;

namespace CodeBreaker.Services.Report.WebApi.Services;
internal interface IStatisticsService
{
    Task<Statistics> GetStatisticsAsync(GetStatisticsArgs args, CancellationToken cancellationToken = default);
}