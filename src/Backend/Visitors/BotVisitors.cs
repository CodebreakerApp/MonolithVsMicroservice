using CodeBreaker.Backend.BotLogic.Runners;
using CodeBreaker.Backend.Data.Models.Bots;
using CodeBreaker.Backend.Services;

namespace CodeBreaker.Backend.Visitors;

public class BotRunVisitor : IBotVisitor<Task>, IDisposable
{
    private readonly IServiceScope _scope;

    private readonly IMoveService _moveService;

    private readonly ILogger _logger;

    private readonly CancellationToken _cancellationToken;

    public BotRunVisitor(IServiceScope scope, CancellationToken cancellationToken = default)
    {
        _scope = scope;
        _cancellationToken = cancellationToken;
        _moveService = scope.ServiceProvider.GetRequiredService<IMoveService>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<BotRunVisitor>>();
    }

    public Task Visit(SimpleBot bot) => new SimpleBotRunner(_moveService, bot, _logger).RunAsync(_cancellationToken);
    
    public void Dispose()
    {
        _scope.Dispose();
    }
}

public class BotNameVisitor : IBotVisitor<string>
{
    public string Visit(SimpleBot bot) => "simple";
}