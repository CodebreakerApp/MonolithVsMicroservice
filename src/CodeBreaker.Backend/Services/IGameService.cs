using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.Services;
public interface IGameService
{
    Task CancelAsync(Game game, CancellationToken cancellationToken = default);
    Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default);
    Task<Game> GetAsync(Guid id, CancellationToken cancellationToken = default);
}