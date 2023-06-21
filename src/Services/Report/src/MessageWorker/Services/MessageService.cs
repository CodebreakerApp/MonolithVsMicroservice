﻿using CodeBreaker.Services.Games.Messaging.Services;
using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;
using CodeBreaker.Services.Report.Data.Repositories;
using CodeBreaker.Services.Report.MessageWorker.Mapping;
using CodeBreaker.Services.Report.MessageWorker.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CodeBreaker.Services.Report.MessageWorker.Services;

internal class MessageService
{
    private readonly IMessageSubscriber _messageSubscriber;

    private readonly IOptions<MessageServiceOptions> _options;

    private readonly IGameRepository _gameRepository;

    private readonly ILogger _logger;

    public MessageService(IMessageSubscriber messageSubscriber, IOptions<MessageServiceOptions> options, IGameRepository gameRepository, ILogger<MessageService> logger)
    {
        _messageSubscriber = messageSubscriber;
        _options = options;
        _gameRepository = gameRepository;
        _logger = logger;
        messageSubscriber.OnGameCreatedAsync += OnGameCreatedCallbackAsync;
        messageSubscriber.OnMoveCreatedAsync += OnMoveCreatedCallbackAsync;
        messageSubscriber.OnGameEndedAsync += OnGameEndedCallbackAsync;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        await _messageSubscriber.StartAsync(cancellationToken);

        while (_options.Value.StopAfterSecondsOfNoMessage < 0) // A value < 0 means, that the service should never stop. But the option may change.
            await Task.Delay(TimeSpan.FromMinutes(10), cancellationToken);

        while (_messageSubscriber.NoMessageDuration == null || _messageSubscriber.NoMessageDuration < TimeSpan.FromSeconds(_options.Value.StopAfterSecondsOfNoMessage))
            await Task.Delay(1000, cancellationToken);

        await _messageSubscriber.StopAsync(cancellationToken);
    }

    private async Task OnGameCreatedCallbackAsync(GameCreatedPayload payload, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnGameCreated");
        await _gameRepository.CreateAsync(payload.ToModel(), cancellationToken);
    }

    private async Task OnMoveCreatedCallbackAsync(MoveCreatedPayload payload, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnMoveCreated");
        await _gameRepository.AddMoveAsync(payload.GameId, payload.ToMoveModel(), cancellationToken);
    }

    private async Task OnGameEndedCallbackAsync(GameEndedPayload payload, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnGameEnded");
        var game = await _gameRepository.GetAsync(payload.Id, cancellationToken);
        payload.ToModel(game);
        await _gameRepository.UpdateAsync(payload.Id, game, cancellationToken);
    }
}