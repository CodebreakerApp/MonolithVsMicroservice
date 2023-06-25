using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.Fields;
using CodeBreaker.Services.Games.Data.Repositories;
using CodeBreaker.Services.Games.GameLogic.Extensions;
using CodeBreaker.Services.Games.GameLogic.Visitors;
using CodeBreaker.Services.Games.Mapping;
using CodeBreaker.Services.Games.Messaging.Services;
using CodeBreaker.Services.Games.Services.Exceptions;

namespace CodeBreaker.Services.Games.Services;

internal class MoveService(IGameRepository gameRepository, IMessagePublisher messagePublisher, ILogger<MoveService> logger) : IMoveService
{
    public async Task<Game> ApplyMoveAsync(Guid gameId, IReadOnlyList<Field> guessPegs, CancellationToken cancellationToken = default)
    {
        var game = await gameRepository.GetAsync(gameId, cancellationToken);

        if (game.HasEnded())
            throw new GameAlreadyEndedException(gameId);

        var appliedMove = game.ApplyMove(guessPegs);

        await gameRepository.UpdateAsync(gameId, game, cancellationToken);
        messagePublisher.PublishMoveCreatedAsync(appliedMove.ToMoveCreatedPayload(gameId), cancellationToken)
            .FireAndForgetButLogExceptions(logger);

        if (game.HasEnded())
            messagePublisher.PublishGameEndedAsync(game.ToGameEndedPayload(), cancellationToken)
                .FireAndForgetButLogExceptions(logger);

        return game;
    }
}