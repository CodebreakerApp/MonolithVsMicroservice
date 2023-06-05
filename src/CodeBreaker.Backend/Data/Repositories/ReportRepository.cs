using CodeBreaker.Backend.Data.DatabaseContexts;
using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Backend.Data.Repositories;

public class ReportRepository(CodeBreakerDbContext dbContext) : IReportRepository
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
        var gameTypeName = args.GameType?.GetName();
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
            .FirstAsync(cancellationToken);
    }

    public IAsyncEnumerable<Game> GetGamesAsync(GetGamesArgs args, CancellationToken cancellationToken = default)
    {
        return dbContext.Games
            .Where(x => x.Start >= args.From && x.Start < args.To)
            .AsAsyncEnumerable();
    }

    public async Task<Game> GetGameAsync(GetGameArgs args, CancellationToken cancellationToken = default)
    {
        return await dbContext.Games
            .Where(x => x.Id == args.Id)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException($"The game with the id {args.Id} was not found");
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

    public GameType? GameType { get; init; }
}

public class GetGamesArgs
{
    private DateTime _to = DateTime.Today.AddDays(1);
    private int _maxCount = 1000;

    public GetGamesArgs()
    {
    }

    public GetGamesArgs(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    public DateTime From { get; protected init; } = DateTime.Today.AddDays(-10);

    public DateTime To
    {
        get => _to;
        init
        {
            if (value < From)
                throw new ArgumentOutOfRangeException(nameof(To), $"Must not be smaller than {nameof(From)}");

            _to = value;
        }
    }

    public int MaxCount
    {
        get => _maxCount;
        init
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(MaxCount), "Must not be smaller than 0");

            _maxCount = value;
        }
    }
}

public class GetGameArgs
{
    public required int Id { get; init; }
}