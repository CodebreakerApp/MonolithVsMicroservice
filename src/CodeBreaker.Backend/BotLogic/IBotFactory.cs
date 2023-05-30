using CodeBreaker.Backend.Data.Models.Bots;

namespace CodeBreaker.Backend.BotLogic;
public interface IBotFactory
{
    Bot CreateBot(string name, BotFactoryArgumnets args);
}