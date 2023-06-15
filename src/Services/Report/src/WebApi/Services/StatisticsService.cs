using CodeBreaker.Services.Report.Data.Models;
using CodeBreaker.Services.Report.Data.Repositories;

namespace CodeBreaker.Services.Report.WebApi.Services;

internal class StatisticsService(IStatisticsRepository repository) : IStatisticsService
{
    public async Task<Statistics> GetStatisticsAsync(GetStatisticsArgs args, CancellationToken cancellationToken = default) =>
        await repository.GetStatisticsAsync(new(args.From, args.To), cancellationToken);
}

internal class GetStatisticsArgs
{
    public DateTime From { get; init; }

    public DateTime To { get; init; } = DateTime.Now;

    public string? GameType { get; init; }
}