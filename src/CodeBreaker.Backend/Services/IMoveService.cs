using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.Services;
public interface IMoveService
{
    Task ApplyMoveAsync(int gameId, Move move, CancellationToken cancellationToken);
}