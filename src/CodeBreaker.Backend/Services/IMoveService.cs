using CodeBreaker.Backend.Data.Models;

namespace CodeBreaker.Backend.Services;
public interface IMoveService
{
    Task ApplyMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken);
}