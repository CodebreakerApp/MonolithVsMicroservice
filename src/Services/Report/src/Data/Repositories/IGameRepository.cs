using CodeBreaker.Services.Report.Data.Models;

namespace CodeBreaker.Services.Report.Data.Repositories;
public interface IGameRepository
{
    Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid gameId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid gameId, Game game, CancellationToken cancellationToken = default);
    Task AddMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken = default);
}