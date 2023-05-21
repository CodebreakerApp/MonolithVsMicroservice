using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Repositories;
using CodeBreaker.Backend.GameLogic.Extensions;
using CodeBreaker.Common.Exceptions;

namespace CodeBreaker.Backend.Services;

public class MoveService(IGameRepository gameRepository) : IMoveService
{
    public async Task<Game> ApplyMoveAsync(int gameId, List<Field> guessPegs, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetAsync(gameId, cancellationToken);

        if (game.HasEnded())
            throw new GameAlreadyEndedException(gameId);

        game.ApplyMove(guessPegs);
        await gameRepository.UpdateAsync(gameId, game, cancellationToken);
        return game;
    }
}
