using CodeBreaker.Services.Bot.Data.Repositories;
using CodeBreaker.Services.Bot.Messaging.Services;
using CodeBreaker.Services.Bot.WebApi.Mapping;
using CodeBreaker.Services.Bot.WebApi.Services.Args;

namespace CodeBreaker.Services.Bot.WebApi.Services;

internal class BotService(IBotFactory botFactory, IBotRepository repository, IBotScheduler botScheduler) : IBotService
{
    public IAsyncEnumerable<Data.Models.Bot> GetBotsAsync(GetBotsArgs args) =>
        repository.GetBotsAsync(new(args.From, args.To) { MaxCount = args.MaxCount });

    public async Task<Data.Models.Bot> GetBotAsync(Guid id, CancellationToken cancellationToken) =>
        await repository.GetBotAsync(id, cancellationToken);

    public async Task<Data.Models.Bot> CreateAndScheduleBotAsync(CreateBotArgs args, CancellationToken cancellationToken = default)
    {
        var bot = botFactory.CreateBot(new (args.Name, args.BotType, args.GameType, args.ThinkTime));
        await repository.CreateAsync(bot, cancellationToken);
        await botScheduler.ScheduleBotAsync(bot.ToBotScheduledPayload(), cancellationToken);
        return bot;
    }
}