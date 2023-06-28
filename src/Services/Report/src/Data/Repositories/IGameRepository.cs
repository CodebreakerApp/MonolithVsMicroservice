using CodeBreaker.Services.Report.Data.Models;
using CodeBreaker.Services.Report.Data.Repositories.Args;

namespace CodeBreaker.Services.Report.Data.Repositories;
public interface IGameRepository : IDisposable
{
    Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid gameId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Game> GetAsync(GetGamesArgs args, CancellationToken cancellationToken = default);
    Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid gameId, Game game, CancellationToken cancellationToken = default);
    Task AddMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken = default);
}