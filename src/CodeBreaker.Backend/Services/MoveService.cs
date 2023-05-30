using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Repositories;
using CodeBreaker.Backend.GameLogic.Extensions;
using CodeBreaker.Backend.SignalRHubs;
using CodeBreaker.Backend.SignalRHubs.Models;
using CodeBreaker.Common.Exceptions;

namespace CodeBreaker.Backend.Services;

public class MoveService(IGameRepository gameRepository, ILiveHubSender liveHubSender) : IMoveService
{
    public async Task<Game> ApplyMoveAsync(int gameId, List<Field> guessPegs, CancellationToken cancellationToken = default)
    {
        var game = await gameRepository.GetAsync(gameId, cancellationToken);

        if (game.HasEnded())
            throw new GameAlreadyEndedException(gameId);

        var appliedMove = game.ApplyMove(guessPegs);
        await gameRepository.UpdateAsync(gameId, game, cancellationToken);
        MoveMadePayload payload = new(gameId, appliedMove);
        await liveHubSender.FireMoveMadeAsync(payload, cancellationToken);
        return game;
    }
}
