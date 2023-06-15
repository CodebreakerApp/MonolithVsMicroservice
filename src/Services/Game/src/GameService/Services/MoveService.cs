using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.Fields;
using CodeBreaker.Services.Games.Data.Repositories;
using CodeBreaker.Services.Games.GameLogic.Extensions;
using CodeBreaker.Services.Games.GameLogic.Visitors;
using CodeBreaker.Services.Games.Mapping;
using CodeBreaker.Services.Games.Messaging.Services;
using CodeBreaker.Services.Games.Services.Exceptions;

namespace CodeBreaker.Services.Games.Services;

internal class MoveService(IGameRepository gameRepository, IMessagePublisher messagePublisher) : IMoveService
{
    public async Task<Game> ApplyMoveAsync(int gameId, IReadOnlyList<Field> guessPegs, CancellationToken cancellationToken = default)
    {
        var game = await gameRepository.GetAsync(gameId, cancellationToken);

        if (game.HasEnded())
            throw new GameAlreadyEndedException(gameId);

        game.ApplyMove(guessPegs);
        await gameRepository.UpdateAsync(gameId, game, cancellationToken);
        var appliedMove = game.Moves.Last();
        await messagePublisher.PublishMoveCreatedAsync(appliedMove.ToMoveCreatedPayload(gameId), cancellationToken);

        if (game.HasEnded())
            await messagePublisher.PublishGameEndedAsync(game.ToGameEndedPayload(), cancellationToken);

        return game;
    }
}
