using CodeBreaker.Services.Report.Data.Models;
using CodeBreaker.Services.Report.Data.Repositories;

namespace CodeBreaker.Services.Report.WebApi.Services;

internal class GameService(IGameRepository repository) : IGameService
{
    public IAsyncEnumerable<Game> GetGamesAsync(GetGamesArgs args, CancellationToken cancellationToken = default) =>
        repository.GetAsync(new Data.Repositories.Args.GetGamesArgs (args.From, args.To) { MaxCount = args.MaxCount }, cancellationToken);

    public async Task<Game> GetGameAsync(GetGameArgs args, CancellationToken cancellationToken = default) =>
        await repository.GetAsync(args.Id, cancellationToken);
}

internal class GetGamesArgs
{
    public DateTime From { get; init; }

    public DateTime To { get; init; } = DateTime.Now;

    public int MaxCount { get; init; } = 1000;
}

internal class GetGameArgs
{
    public required Guid Id { get; init; }
}