using CodeBreaker.Services.Games.Data.Models;

namespace CodeBreaker.Services.Games.Services;

internal interface IGameService
{
    Task CancelAsync(Guid gameId, CancellationToken cancellationToken = default);
    Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default);
    Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default);
}