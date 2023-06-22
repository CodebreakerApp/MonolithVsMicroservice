using CodeBreaker.Services.Games.Messaging.Services;
using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;
using CodeBreaker.Services.Live.SignalRHubs;

namespace CodeBreaker.Services.Live.Services;

internal class MessageWorkerService : IHostedService
{
    private readonly ILogger _logger;

    private readonly IMessageSubscriber _messageSubscriber;

    private readonly ILiveHubSender _sender;

    public MessageWorkerService(IMessageSubscriber messageSubscriber, ILiveHubSender sender, ILogger<MessageWorkerService> logger)
    {
        _messageSubscriber = messageSubscriber;
        _logger = logger;
        _sender = sender;
        messageSubscriber.OnGameCreatedAsync += OnGameCreatedCallbackAsync;
        messageSubscriber.OnMoveCreatedAsync += OnMoveCreatedCallbackAsync;
        messageSubscriber.OnGameEndedAsync += OnGameEndedCallbackAsync;
    }

    public async Task StartAsync(CancellationToken cancellationToken) =>
        await _messageSubscriber.StartAsync(cancellationToken);

    public async Task StopAsync(CancellationToken cancellationToken) =>
        await _messageSubscriber.StopAsync(cancellationToken);

    private async Task OnGameCreatedCallbackAsync(GameCreatedPayload payload, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnGameCreated");
        await _sender.FireGameCreatedAsync(payload, cancellationToken);
    }

    private async Task OnMoveCreatedCallbackAsync(MoveCreatedPayload payload, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnMoveCreated");
        await _sender.FireMoveMadeAsync(payload, cancellationToken);
    }

    private async Task OnGameEndedCallbackAsync(GameEndedPayload payload, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnGameEnded");
        await _sender.FireGameEndedAsync(payload, cancellationToken);
    }
}
