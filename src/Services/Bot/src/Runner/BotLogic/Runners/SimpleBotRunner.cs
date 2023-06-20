using CodeBreaker.Services.Bot.Runner.BotLogic.Exceptions;
using CodeBreaker.Services.Bot.Runner.BotLogic.Runners.Extensions;
using CodeBreaker.Services.Bot.Runner.Models;
using CodeBreaker.Services.Bot.Runner.Models.Bots;
using CodeBreaker.Services.Bot.Runner.Models.Fields;
using CodeBreaker.Services.Bot.Runner.Models.GameTypes;
using CodeBreaker.Services.Bot.Runner.Services;
using Microsoft.Extensions.Logging;

namespace CodeBreaker.Services.Bot.Runner.BotLogic.Runners;

internal class SimpleBotRunner
{
    private readonly SimpleBot? _bot;

    private readonly ILogger _logger;

    public SimpleBotRunner(IMoveService moveService, SimpleBot bot, ILogger logger)
    {
        MoveService = moveService;
        Bot = bot;
        _logger = logger;
    }

    protected IMoveService MoveService { get; init; }

    protected SimpleBot Bot
    {
        get => _bot!;
        init
        {
            if (value.GameId == default)
                throw new ArgumentException(nameof(value.GameId), "The gameid of the bot must not be default.");

            if (value.Game == default)
                throw new ArgumentNullException(nameof(Bot.Game), "The bots game must not be null.");

            _bot = value;
        }
    }

    protected Game Game => Bot.Game!;

    /// <summary>
    /// Runs the bot.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="InvalidOperationException">Occurs, when the keypegs returned by the game logic are null (should not be the case).</exception>
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var slots = Game.Type.GetRandomFields().ToArray();
        var firstRun = Bot.InitializeFieldRuns(MoveService, slots);
        var game = await MoveService.ApplyMoveAsync(Game, slots, cancellationToken);
        var keyPegs = game.Moves.Last().KeyPegs ?? throw new InvalidOperationException("KeyPegs must not be null");
        _logger.LogDebug("Game {gameId} started", game.Id);

        try
        {
            var result = await firstRun.RunAsync(keyPegs, cancellationToken).ToArrayAsync();
            _logger.LogInformation("Completed game {gameId}: {code}", game.Id, result);
        }
        catch (GameEndedException e) when (e.GameState == GameState.Lost)
        {
            _logger.LogInformation(e, "Lost the game {gameId}. Could not solve the code.", e.Game.Id);
            throw;
        }
        catch (GameEndedException e)
        {
            _logger.LogError(e, "The game {gameId} has already ended with state {gameState} and can therefore not be played.", e.Game.Id, e.GameState);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception while running bot {botId}", Bot.Id);
            throw;
        }
    }
}

file static class SimpleBotExtensions
{
    public static IFieldRun InitializeFieldRuns(this SimpleBot bot, IMoveService moveService, Field[] slots) =>
        bot.Game?.Type.Accept(new SimpleBotRunInitializeVisitor(moveService, bot, slots))
            ?? throw new InvalidOperationException("The gametype of the game to play must be set.");
}

file class SimpleBotRunInitializeVisitor(IMoveService moveService, SimpleBot bot, Field[] slots) : IGameTypeVisitor<IFieldRun>
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