using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Repositories;
using CodeBreaker.Backend.SignalRHubs;
using CodeBreaker.Backend.SignalRHubs.Models;

namespace CodeBreaker.Backend.Services;

public class GameService(IGameRepository repository, ILiveHubSender liveHubSender) : IGameService
{
    public IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default) =>
        repository.GetAsync(cancellationToken);

    public Task<Game> GetAsync(int gameId, CancellationToken cancellationToken = default) =>
        repository.GetAsync(gameId, cancellationToken);

    public async Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        await repository.CreateAsync(game, cancellationToken);
        await liveHubSender.FireGameCreatedAsync(new(game), cancellationToken);
        return game;
    }

    public async Task CancelAsync(int gameId, CancellationToken cancellationToken = default)
    {
        await repository.CancelAsync(gameId, cancellationToken);
        var game = await repository.GetAsync(gameId, cancellationToken);

        if (game.End == null)
            throw new InvalidOperationException("The 'end' of the game is null, even after it was cancelled.");

        GameEndedPayload payload = new(gameId, game.Won, game.Cancelled, game.End.Value);
        await liveHubSender.FireGameEndedAsync(payload, cancellationToken);
    }

    public async Task DeleteAsync(int gameId, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync(gameId, cancellationToken);
        var game = await repository.GetAsync(gameId, cancellationToken);

        GameEndedPayload payload = new(gameId, game.Won, game.Cancelled, game.End ?? DateTime.Now);
        await liveHubSender.FireGameEndedAsync(payload, cancellationToken);
    }
}
