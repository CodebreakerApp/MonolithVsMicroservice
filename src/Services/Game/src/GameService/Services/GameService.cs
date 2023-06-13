using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Repositories;

namespace CodeBreaker.Services.Games.Services;

internal class GameService(IGameRepository repository) : IGameService
{
    public IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default) =>
        repository.GetAsync(cancellationToken);

    public Task<Game> GetAsync(int gameId, CancellationToken cancellationToken = default) =>
        repository.GetAsync(gameId, cancellationToken);

    public async Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        await repository.CreateAsync(game, cancellationToken);
        return game;
    }

    public async Task CancelAsync(int gameId, CancellationToken cancellationToken = default)
    {
        await repository.CancelAsync(gameId, cancellationToken);
        var game = await repository.GetAsync(gameId, cancellationToken);

        if (game.End == null)
            throw new InvalidOperationException("The 'end' of the game is null, even after it was cancelled.");
    }

    public async Task DeleteAsync(int gameId, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync(gameId, cancellationToken);
        var game = await repository.GetAsync(gameId, cancellationToken);
    }
}
