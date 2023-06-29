using CodeBreaker.Services.Bot.Data.Models;
using CodeBreaker.Services.Bot.Data.Repositories.Args;

namespace CodeBreaker.Services.Bot.Data.Repositories;
public interface IBotRepository : IDisposable
{
    Task<Models.Bot> CreateAsync(Models.Bot bot, CancellationToken cancellationToken = default);
    Task<Models.Bot> GetBotAsync(Guid id, CancellationToken cancellationToken);
    IAsyncEnumerable<Models.Bot> GetBotsAsync(GetBotsArgs args);
    Task UpdateStateAsync(Guid id, BotState newState, CancellationToken cancellationToken = default);
    Task UpdateGameIdAsync(Guid botId, Guid gameId, CancellationToken cancellationToken = default);
}