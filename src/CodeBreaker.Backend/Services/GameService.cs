using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Repositories;

namespace CodeBreaker.Backend.Services;

public class GameService(IGameRepository repository) : IGameService
{
    public IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default) =>
        repository.GetAsync(cancellationToken);

    public Task<Game> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
        repository.GetAsync(id, cancellationToken);

    public Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default) =>
        repository.CreateAsync(game, cancellationToken);

    public Task CancelAsync(Game game, CancellationToken cancellationToken = default) =>
        repository.CancelAsync(game, cancellationToken);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        repository.DeleteAsync(id, cancellationToken);
}
