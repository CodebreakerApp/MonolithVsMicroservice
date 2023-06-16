using CodeBreaker.Services.Games.Data.Models;

namespace CodeBreaker.Services.Games.Data.Repositories;
public interface IGameRepository
{
    Task AddMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken = default);
    Task CancelAsync(Guid gameId, CancellationToken cancellationToken = default);
    Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid gameId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default);
    Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid gameId, Game game, CancellationToken cancellationToken = default);
}