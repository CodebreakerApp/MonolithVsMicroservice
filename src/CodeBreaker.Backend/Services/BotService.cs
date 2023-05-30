using CodeBreaker.Backend.BotLogic;
using CodeBreaker.Backend.Data.Models.Bots;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Data.Repositories;

namespace CodeBreaker.Backend.Services;

public class BotService(IBotFactory botFactory, IBotRepository botRepository, IGameRepository gameRepository, IBotRunnerService botRunnerService) : IBotService
{
    public async Task<Bot> CreateBotAsync(CreateBotArgs args, CancellationToken cancellationToken = default)
    {
        var game = args.GameType.CreateGame(new("Bot"));
        await gameRepository.CreateAsync(game, cancellationToken);

        var bot = botFactory.CreateBot(args.botName, new (game, args.ThinkTime));
        bot = await botRepository.CreateAsync(bot, cancellationToken);
        botRunnerService.ScheduleRun(bot);
        return bot;
    }
}

public record class CreateBotArgs(string botName, GameType GameType, int ThinkTime);