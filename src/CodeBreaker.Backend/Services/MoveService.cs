using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Repositories;

namespace CodeBreaker.Backend.Services;

public class MoveService(IGameRepository gameRepository) : IMoveService
{
    public Task ApplyMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken)
    {
        // game logic here
        return gameRepository.AddMoveAsync(gameId, move, cancellationToken);
    }
}
