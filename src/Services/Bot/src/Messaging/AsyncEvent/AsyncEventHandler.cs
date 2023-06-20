namespace CodeBreaker.Services.Bot.Messaging.AsyncEvent;

public delegate Task AsyncEventHandler<TArgs>(TArgs args, CancellationToken cancellationToken = default);