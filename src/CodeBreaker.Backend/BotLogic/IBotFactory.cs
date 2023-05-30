using CodeBreaker.Backend.Data.Models.Bots;

namespace CodeBreaker.Backend.BotLogic;
public interface IBotFactory
{
    IReadOnlyList<string> AvailableBotNames { get; }
    Bot CreateBot(string name, BotFactoryArgumnets args);
}