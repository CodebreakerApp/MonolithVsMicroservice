using CodeBreaker.Backend.BotLogic.Runners;
using CodeBreaker.Backend.Data.Models.Bots;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Services;

namespace CodeBreaker.Backend.Visitors;

public class SimpleBotRunInitializeVisitor(IMoveService moveService, SimpleBot bot, Field[] slots) : IGameTypeVisitor<IFieldRun>
{
    public IFieldRun Visit(GameType6x4 gameType) =>
        SimpleBotRunInit(0, gameType.Holes) ?? throw new InvalidOperationException("Could not create a FieldRun.");

    public IFieldRun Visit(GameType8x5 gameType) =>
        SimpleBotRunInit(0, gameType.Holes) ?? throw new InvalidOperationException("Could not create a FieldRun.");

    /// <summary>
    /// Recursively initializes the runs.
    /// </summary>
    /// <param name="index">The index of the run.</param>
    /// <param name="limit">The limit of the runs. (aka the maximum number of runs; used to stop the recursion)</param>
    /// <returns>The first run (linking to the subsequent run, ...).</returns>
    private IFieldRun? SimpleBotRunInit(int index, int limit)
    {
        if (index == limit) return null;
        return new SimpleBotDefaultFieldRun(moveService, bot, slots, index, SimpleBotRunInit(index + 1, limit));
    }
}