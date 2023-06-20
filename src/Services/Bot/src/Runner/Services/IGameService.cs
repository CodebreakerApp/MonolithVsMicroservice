using CodeBreaker.Services.Bot.Runner.Models;
using CodeBreaker.Services.Bot.Runner.Services.Args;

namespace CodeBreaker.Services.Bot.Runner.Services;
internal interface IGameService
{
    Task<Game> CreateGameAsync(CreateGameArgs args, CancellationToken cancellationToken = default);
}