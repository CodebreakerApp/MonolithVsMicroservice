using CodeBreaker.Backend.Data.Models;
using System.Runtime.CompilerServices;

namespace CodeBreaker.Backend.Data.Repositories;
public interface IGameRepository
{
    Task AddMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken = default);
    Task CancelAsync(Game game, CancellationToken cancellationToken = default);
    Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid gameId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default);
    Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Game game, CancellationToken cancellationToken = default);
}