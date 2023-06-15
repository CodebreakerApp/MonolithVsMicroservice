using CodeBreaker.Services.Report.Data.DatabaseContexts;
using CodeBreaker.Services.Report.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Services.Report.Data.Repositories;

public class StatisticsRepository(ReportDbContext dbContext) : IStatisticsRepository
{
    private class StatisticsResult
    {
        public int TotalGames { get; set; }
        public int WonGames { get; set; }
        public int CancelledGames { get; set; }
        public int MinGameDuration { get; set; }
        public int MaxGameDuration { get; set; }
        public int AvgGameDuration { get; set; }
        public int MinMoveCount { get; set; }
        public int MaxMoveCount { get; set; }
        public int AvgMoveCount { get; set; }
    };
    public async Task<Statistics> GetStatisticsAsync(GetStatisticsArgs args, CancellationToken cancellationToken = default)
    {
        var gameTypeName = args.GameType;
        return await dbContext.Database
            .SqlQuery<StatisticsResult>($"SELECT * FROM QueryStatistics({args.From}, {args.To}, {gameTypeName})")
            .Select(result => new Statistics()
            {
                TotalGames = result.TotalGames,
                WonGames = result.WonGames,
                CancelledGames = result.CancelledGames,
                MinGameDuration = TimeSpan.FromMilliseconds(result.MinGameDuration),
                MaxGameDuration = TimeSpan.FromMilliseconds(result.MaxGameDuration),
                AvgGameDuration = TimeSpan.FromMilliseconds(result.AvgGameDuration),
                MinMoveCount = result.MinMoveCount,
                MaxMoveCount = result.MaxMoveCount,
                AvgMoveCount = result.AvgMoveCount,
            })
            .SingleAsync(cancellationToken);
    }
}


public class GetStatisticsArgs
{
    public GetStatisticsArgs()
    {
    }

    public GetStatisticsArgs(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    private DateTime _to = DateTime.Today.AddDays(1);

    public DateTime From { get; protected init; } = DateTime.Today.AddDays(-10);

    public DateTime To
    {
        get => _to;
        protected init
        {
            if (value < From)
                throw new ArgumentOutOfRangeException(nameof(To), $"Must not be smaller than {nameof(From)}");
            _to = value;
        }
    }

    public string? GameType { get; init; }
}