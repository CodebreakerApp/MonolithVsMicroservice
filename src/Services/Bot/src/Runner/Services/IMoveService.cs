using CodeBreaker.Services.Bot.Runner.Models;
using CodeBreaker.Services.Bot.Runner.Models.Fields;

namespace CodeBreaker.Services.Bot.Runner.Services;

internal interface IMoveService
{
    Task<Game> ApplyMoveAsync(Game game, IReadOnlyList<Field> guessPegs, CancellationToken cancellationToken = default);
}
