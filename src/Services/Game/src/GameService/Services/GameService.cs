using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Repositories;
using CodeBreaker.Services.Games.Mapping;
using CodeBreaker.Services.Games.Messaging.Services;

namespace CodeBreaker.Services.Games.Services;

internal class GameService(IGameRepository repository, IMessagePublisher messagePublisher) : IGameService
{
    public IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default) =>
        repository.GetAsync(cancellationToken);

    public Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default) =>
        repository.GetAsync(gameId, cancellationToken);

    public async Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        await repository.CreateAsync(game, cancellationToken);
        await messagePublisher.PublishGameCreatedAsync(game.ToGameCreatedPayload(), cancellationToken);
        return game;
    }

    public async Task CancelAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        await repository.CancelAsync(gameId, cancellationToken);
        var game = await repository.GetAsync(gameId, cancellationToken);

        if (game.End == null)
            throw new InvalidOperationException("The 'end' of the game is null, even after it was cancelled.");

        await messagePublisher.PublishGameEndedAsync(game.ToGameEndedPayload(), cancellationToken);
    }
}
