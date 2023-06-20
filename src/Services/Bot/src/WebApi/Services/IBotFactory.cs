namespace CodeBreaker.Services.Bot.WebApi.Services;

internal interface IBotFactory
{
    IReadOnlyList<string> AvailableBotNames { get; }

    Data.Models.Bot CreateBot(BotFactoryArgumnets args);
}