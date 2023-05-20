using CodeBreaker.Backend.Data.Models;
using System.Runtime.CompilerServices;

namespace CodeBreaker.Backend.Data.Repositories;
public interface IGameRepository
{
    Task AddMoveAsync(int gameId, Move move, CancellationToken cancellationToken = default);
    Task CancelAsync(int gameId, CancellationToken cancellationToken = default);
    Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(int gameId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default);
    Task<Game> GetAsync(int gameId, CancellationToken cancellationToken = default);
    Task UpdateAsync(int gameId, Game game, CancellationToken cancellationToken = default);
}