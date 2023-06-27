using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Data.Repositories;

namespace CodeBreaker.Backend.Services;

public class ReportService(IReportRepository reportRepository) : IReportService
{
    public async Task<Statistics> GetStatisticsAsync(GetStatisticsArgs args, CancellationToken cancellationToken = default)
    {
        return await reportRepository.GetStatisticsAsync(new(args.From, args.To)
        {
            GameType = args.GameType,
        }, cancellationToken);
    }

    public IAsyncEnumerable<Game> GetGamesAsync(GetGamesArgs args, CancellationToken cancellationToken = default)
    {
        return reportRepository.GetGamesAsync(new(args.From, args.To)
        {
            MaxCount = args.MaxCount,
        }, cancellationToken);
    }

    public async Task<Game> GetGameAsync(GetGameArgs args, CancellationToken cancellationToken = default)
    {
        return await reportRepository.GetGameAsync(new()
        {
            Id = args.Id,
        }, cancellationToken);
    }
}


public class GetStatisticsArgs
{
    public DateTime From { get; init; }

    public DateTime To { get; init; } = DateTime.Now;

    public GameType? GameType { get; init; }
}

public class GetGamesArgs
{
    public DateTime From { get; init; }

    public DateTime To { get; init; } = DateTime.Now;

    public int MaxCount { get; init; } = 100;
}

public class GetGameArgs
{
    public required int Id { get; init; }
}