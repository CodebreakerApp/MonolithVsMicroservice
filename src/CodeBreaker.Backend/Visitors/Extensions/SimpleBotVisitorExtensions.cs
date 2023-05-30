using CodeBreaker.Backend.BotLogic.Runners;
using CodeBreaker.Backend.Data.Models.Bots;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Services;

namespace CodeBreaker.Backend.Visitors.Extensions;

public static class SimpleBotExtensions
{
    public static IFieldRun InitializeFieldRuns(this SimpleBot bot, IMoveService moveService, List<Field> slots) =>
        bot.Game?.Type.Accept(new SimpleBotRunInitializeVisitor(moveService, bot, slots))
            ?? throw new InvalidOperationException("The gametype of the game to play must be set.");
}