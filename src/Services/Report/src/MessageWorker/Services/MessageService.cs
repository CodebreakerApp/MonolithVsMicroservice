using CodeBreaker.Service.Report.MessageWorker.Options;
using CodeBreaker.Services.Games.Messaging.Services;
using CodeBreaker.Services.Games.Transfer.Messaging.Payloads;
using Microsoft.Extensions.Options;

namespace CodeBreaker.Service.Report.MessageWorker.Services;

internal class MessageService
{
    private readonly IMessageSubscriber _messageSubscriber;

    private readonly IOptions<MessageServiceOptions> _options;

    public MessageService(IMessageSubscriber messageSubscriber, IOptions<MessageServiceOptions> options)
    {
        _messageSubscriber = messageSubscriber;
        _options = options;
        messageSubscriber.OnGameCreatedAsync += OnGameCreatedCallbackAsync;
        messageSubscriber.OnMoveCreatedAsync += OnMoveCreatedCallbackAsync;
        messageSubscriber.OnGameEndedAsync += OnGameEndedCallbackAsync;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        await _messageSubscriber.StartAsync(cancellationToken);
        
        while (_messageSubscriber.NoMessageDuration < TimeSpan.FromSeconds(_options.Value.StopAfterSecondsOfNoMessage))
            await Task.Delay(1000);

        await _messageSubscriber.StopAsync(cancellationToken);
    }

    private Task OnGameCreatedCallbackAsync(GameCreatedPayload payload)
    {
        throw new NotImplementedException();
    }

    private Task OnMoveCreatedCallbackAsync(MoveCreatedPayload payload)
    {
        throw new NotImplementedException();
    }

    private Task OnGameEndedCallbackAsync(GameEndedPayload payload)
    {
        throw new NotImplementedException();
    }
}