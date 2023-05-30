using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.Services;
public interface IMoveService
{
    Task<Game> ApplyMoveAsync(int gameId, List<Field> guessPegs, CancellationToken cancellationToken = default);
}