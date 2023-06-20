using CodeBreaker.Services.Bot.Runner.BotLogic.Runners;
using CodeBreaker.Services.Bot.Runner.Models.Bots;
using CodeBreaker.Services.Bot.Runner.Services;
using Microsoft.Extensions.Logging;

namespace CodeBreaker.Services.Bot.Runner.BotLogic;

internal class BotRunVisitor(IMoveService moveService, ILogger logger, CancellationToken cancellationToken = default) : IBotVisitor<Task>
{
    public Task Visit(SimpleBot bot) => new SimpleBotRunner(moveService, bot, logger).RunAsync(cancellationToken);
}

internal static class BotRunVisitorExtensions
{
    public static Task RunAsync(this Models.Bots.Bot bot, IMoveService moveService, ILogger logger, CancellationToken cancellationToken = default) =>
        bot.Accept(new BotRunVisitor(moveService, logger, cancellationToken));
}