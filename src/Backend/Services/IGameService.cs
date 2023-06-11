using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.Services;
public interface IGameService
{
    Task CancelAsync(int gameId, CancellationToken cancellationToken = default);
    Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(int gameId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default);
    Task<Game> GetAsync(int gameId, CancellationToken cancellationToken = default);
}