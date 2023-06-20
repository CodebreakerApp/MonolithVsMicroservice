using CodeBreaker.Services.Bot.WebApi.Services.Args;

namespace CodeBreaker.Services.Bot.WebApi.Services;
internal interface IBotService
{
    Task<Data.Models.Bot> CreateAndScheduleBotAsync(CreateBotArgs args, CancellationToken cancellationToken = default);
    Task<Data.Models.Bot> GetBotAsync(Guid id, CancellationToken cancellationToken);
    IAsyncEnumerable<Data.Models.Bot> GetBotsAsync(GetBotsArgs args);
}