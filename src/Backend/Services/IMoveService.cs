using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.Services;
public interface IMoveService
{
    Task<Game> ApplyMoveAsync(int gameId, IReadOnlyList<Field> guessPegs, CancellationToken cancellationToken = default);
}