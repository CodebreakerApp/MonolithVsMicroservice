namespace CodeBreaker.Services.Games.Messaging.AsyncEvent;

public delegate Task AsyncEventHandler<TArgs>(TArgs args, CancellationToken cancellationToken = default);