using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Bots;
using CodeBreaker.Common.Exceptions;
using System.Collections.Frozen;

namespace CodeBreaker.Backend.BotLogic;

public class BotFactory : IBotFactory
{
    private static readonly FrozenDictionary<string, Func<BotFactoryArgumnets, Bot>> s_mapping = new Func<BotFactoryArgumnets, Bot>[]
        {
            CreateSimpleBot
        }.ToFrozenDictionary(bot => bot(new DummyBotFactoryArguments()).GetName());

    public IReadOnlyList<string> AvailableBotNames => s_mapping.Keys;

    public Bot CreateBot(string name, BotFactoryArgumnets args) =>
        (s_mapping.GetValueOrDefault(name) ?? throw new NotFoundException())(args);

    private static Bot CreateSimpleBot(BotFactoryArgumnets args) =>
        new SimpleBot()
        {
            ThinkTime = args.ThinkTime,
            Game = args.Game,
            GameId = args.Game?.Id ?? 0,
        };
}

public record class BotFactoryArgumnets(Game Game, int ThinkTime);

file record class DummyBotFactoryArguments() : BotFactoryArgumnets(null!, 0);