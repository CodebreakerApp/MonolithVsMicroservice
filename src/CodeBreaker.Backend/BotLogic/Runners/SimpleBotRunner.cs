using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Bots;
using CodeBreaker.Backend.Services;
using CodeBreaker.Backend.Visitors.Extensions;
using CodeBreaker.Common.Exceptions;

namespace CodeBreaker.Backend.BotLogic.Runners;

public class SimpleBotRunner
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
    /// <exception cref="System.InvalidOperationException">Occurs, when the keypegs returned by the game logic are null (should not be the case).</exception>
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var slots = Game.Type.GetRandomFields().ToList();
        var firstRun = Bot.InitializeFieldRuns(MoveService, slots);
        var game = await MoveService.ApplyMoveAsync(Game.Id, slots, cancellationToken);
        var keyPegs = game.Moves.Last().KeyPegs ?? throw new InvalidOperationException("KeyPegs must not be null");
        _logger.LogDebug("Game {gameId} started with code {code}", game.Id, game.Code);

        try
        {
            var result = await firstRun.RunAsync(keyPegs, cancellationToken).ToArrayAsync();
            _logger.LogInformation("Completed game {gameId}: {code}", game.Id, result);
        }
        catch (GameAlreadyEndedException e)
        {
            _logger.LogInformation(e, "Could not complete game {gameId}", game.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception while running bot {botId}", Bot.Id);
            throw;
        }
    }
}
