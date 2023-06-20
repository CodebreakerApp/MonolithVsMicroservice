using CodeBreaker.Services.Bot.Data.Models;
using CodeBreaker.Services.Bot.Visitors;
using CodeBreaker.Services.Bot.WebApi.Services.Exceptions;
using System.Collections.Frozen;

namespace CodeBreaker.Services.Bot.WebApi.Services;

internal class BotFactory : IBotFactory
{
    private static readonly FrozenDictionary<string, Func<BotFactoryArgumnets, Data.Models.Bot>> s_mapping = new Func<BotFactoryArgumnets, Data.Models.Bot>[]
        {
            CreateSimpleBot
        }.ToFrozenDictionary(bot => bot(new DummyBotFactoryArguments()).GetName());

    public IReadOnlyList<string> AvailableBotNames => s_mapping.Keys;

    public Data.Models.Bot CreateBot(BotFactoryArgumnets args) =>
        (s_mapping.GetValueOrDefault(args.BotType) ?? throw new NotFoundException())(args);

    private static Data.Models.Bot CreateSimpleBot(BotFactoryArgumnets args) =>
        new SimpleBot()
        {
            Name = args.Name,
            Type = args.BotType,
            GameType = args.GameType,
            ThinkTime = args.ThinkTime,
            CreatedAt = DateTime.Now,
            State = BotState.Scheduled,
        };
}

internal record class BotFactoryArgumnets(string Name, string BotType, string GameType, int ThinkTime);

file record class DummyBotFactoryArguments() : BotFactoryArgumnets(null!, null!, null!, 0);