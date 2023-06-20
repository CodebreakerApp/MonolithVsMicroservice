namespace CodeBreaker.Services.Bot.Messaging.AsyncEvent;

public static class AsyncEventHandlerExtensions
{
    public static async Task InvokeAsync<TArgs>(this Func<AsyncEventHandler<TArgs>?> eventHandlerResolver, TArgs args, CancellationToken cancellationToken = default)
    {
        var resolvedEvent = eventHandlerResolver();

        if (resolvedEvent == null)
            return;

        await resolvedEvent(args, cancellationToken);
    }

    public static async Task InvokeAsync<TArgs>(this AsyncEventHandler<TArgs>? eventHandler, TArgs args, CancellationToken cancellationToken = default)
    {
        if (eventHandler == null)
            return;

        await eventHandler.Invoke(args, cancellationToken);
    }
}