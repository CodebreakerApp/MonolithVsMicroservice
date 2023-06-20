using CodeBreaker.Services.Bot.Data.Repositories;
using CodeBreaker.Services.Bot.Runner.BotLogic;
using CodeBreaker.Services.Bot.Runner.BotLogic.Exceptions;
using CodeBreaker.Services.Bot.Runner.Models;
using CodeBreaker.Services.Bot.Runner.Models.Bots;
using CodeBreaker.Services.Bot.Runner.Models.GameTypes;
using CodeBreaker.Services.Bot.Runner.Services.Args;
using Microsoft.Extensions.Logging;

namespace CodeBreaker.Services.Bot.Runner.Services;

internal class BotRunnerService
{
    private readonly IBotRepository _botRepository;

    private readonly IGameService _gameService;

    private readonly IMoveService _moveService;

    private readonly IGameTypeService _gameTypeService;

    private readonly ILogger _logger;

    public BotRunnerService(
        IBotRepository botRepository,
        IGameService gameService,
        IMoveService moveService,
        IGameTypeService gameTypeService,
        ILogger<BotRunnerService> logger
    )
    {
        _botRepository = botRepository;
        _gameService = gameService;
        _moveService = moveService;
        _gameTypeService = gameTypeService;
        _logger = logger;
    }

    public async Task RunBotAsync(Messaging.Transfer.Payloads.BotScheduledPayload args, CancellationToken cancellationToken = default)
    {
        GameType gameType;
        try
        {
            gameType = await _gameTypeService.FetchGameTypeAsync(args.GameType, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "The requested gameType {gameType} does not exist or could not be fetched.", args.GameType);
            await MarkAsFailedAsync(args.Id, cancellationToken);
            return;
        }

        CreateGameArgs createGameArgs = new()
        {
            Type = gameType,
            Username = args.Username
        };

        Game game;
        try
        {
            game = await _gameService.CreateGameAsync(createGameArgs, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not create game for bot {botId}.", args.Id);
            await MarkAsFailedAsync(args.Id, cancellationToken);
            throw;
        }

        await _botRepository.UpdateGameIdAsync(args.Id, game.Id, cancellationToken);

        var bot = new SimpleBot() // TODO map bot
        {
            Id = args.Id,
            GameId = game.Id,
            Game = game,
            ThinkTime = args.ThinkTime,
        };

        try
        {
            await bot.RunAsync(_moveService, _logger, cancellationToken);
        }
        catch (GameEndedException)
        {
            _logger.LogInformation("Game ended. Bot run ends...");
            await MarkAsEndedAsync(args.Id, cancellationToken);
            return;
        }
        catch (Exception e)
        {
            _logger.LogInformation(e, "Error ocrrued. Bot will get rescheduled");
            await MarkAsFailedAsync(args.Id, cancellationToken);
            throw;
        }

        await MarkAsEndedAsync(args.Id, cancellationToken);
    }

    private async Task MarkAsFailedAsync(Guid botId, CancellationToken cancellationToken) =>
        await _botRepository.UpdateStateAsync(botId, Data.Models.BotState.Failed, cancellationToken);

    private async Task MarkAsEndedAsync(Guid botId, CancellationToken cancellationToken) =>
        await _botRepository.UpdateStateAsync(botId, Data.Models.BotState.Ended, cancellationToken);
}
