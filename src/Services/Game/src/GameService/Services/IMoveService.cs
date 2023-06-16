using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.Services;

internal interface IMoveService
{
    Task<Game> ApplyMoveAsync(Guid gameId, IReadOnlyList<Field> guessPegs, CancellationToken cancellationToken = default);
}