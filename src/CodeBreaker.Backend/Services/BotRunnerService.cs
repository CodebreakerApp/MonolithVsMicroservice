using CodeBreaker.Backend.Data.Models.Bots;
using CodeBreaker.Backend.Visitors;

namespace CodeBreaker.Backend.Services;

public class BotRunnerService(IServiceProvider services, ILogger<BotRunnerService> logger) : IBotRunnerService
{
    public void ScheduleRun(Bot bot)
    {
        logger.LogInformation("Scheduling run for bot {botId} to solve game {gameId}...", bot.Id, bot.GameId);
        var serviceScope = services.CreateScope();
        Task.Run(() => bot.Accept(new BotRunVisitor(serviceScope)));
    }
}