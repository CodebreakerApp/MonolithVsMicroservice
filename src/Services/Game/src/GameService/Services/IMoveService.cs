using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.Fields;

namespace CodeBreaker.Services.Games.Services;

internal interface IMoveService
{
    Task<Game> ApplyMoveAsync(int gameId, IReadOnlyList<Field> guessPegs, CancellationToken cancellationToken = default);
}