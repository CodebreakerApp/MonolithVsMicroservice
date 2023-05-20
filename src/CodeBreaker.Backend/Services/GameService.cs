using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Repositories;

namespace CodeBreaker.Backend.Services;

public class GameService(IGameRepository repository) : IGameService
{
    public IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default) =>
        repository.GetAsync(cancellationToken);

    public Task<Game> GetAsync(int gameId, CancellationToken cancellationToken = default) =>
        repository.GetAsync(gameId, cancellationToken);

    public Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default) =>
        repository.CreateAsync(game, cancellationToken);

    public Task CancelAsync(int gameId, CancellationToken cancellationToken = default) =>
        repository.CancelAsync(gameId, cancellationToken);

    public Task DeleteAsync(int gameId, CancellationToken cancellationToken = default) =>
        repository.DeleteAsync(gameId, cancellationToken);
}
