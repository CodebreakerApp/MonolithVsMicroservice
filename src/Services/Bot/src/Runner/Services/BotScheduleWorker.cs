using CodeBreaker.Services.Bot.Data.Repositories;
using CodeBreaker.Services.Bot.Messaging.Services;
using CodeBreaker.Services.Bot.Runner.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CodeBreaker.Services.Bot.Runner.Services;

internal class BotScheduleWorker : IDisposable
{
    private readonly IBotConsumer _botConsumer;

    private readonly IOptions<BotScheduleWorkerOptions> _options;

    private readonly IServiceScope _serviceScope;

    public BotScheduleWorker(
        IBotConsumer botConsumer,
        IOptions<BotScheduleWorkerOptions> options,
        IServiceProvider serviceProvider
    )
    {
        _botConsumer = botConsumer;
        _options = options;
        _serviceScope = serviceProvider.CreateScope();
        botConsumer.OnBotScheduledAsync += OnBotScheduledCallbackAsync;
    }

    private IServiceProvider Services => _serviceScope.ServiceProvider;

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        await _botConsumer.StartAsync(cancellationToken);

        while (_botConsumer.NoMessageDuration == null || _botConsumer.NoMessageDuration < TimeSpan.FromSeconds(_options.Value.StopAfterSecondsOfNoMessage))
            await Task.Delay(1000, cancellationToken);

        await _botConsumer.StopAsync(cancellationToken);
    }

    private async Task OnBotScheduledCallbackAsync(Messaging.Transfer.Payloads.BotScheduledPayload args, CancellationToken cancellationToken = default) =>
        await new BotRunnerService(
            Services.GetRequiredService<IBotRepository>(),
            Services.GetRequiredService<IGameService>(),
            Services.GetRequiredService<IMoveService>(),
            Services.GetRequiredService<IGameTypeService>(),
            Services.GetRequiredService<ILogger<BotRunnerService>>()
        ).RunBotAsync(args, cancellationToken);

    public void Dispose() =>
        _serviceScope.Dispose();
}