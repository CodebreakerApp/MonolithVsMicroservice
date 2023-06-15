namespace CodeBreaker.Services.Games.Messaging.AsyncEvent;

public static class AsyncEventHandlerExtensions
{
    public static async Task InvokeAsync<TArgs>(this Func<AsyncEventHandler<TArgs>?> eventHandlerResolver, TArgs args, CancellationToken cancellationToken = default)
    {
        var resolvedEvent = eventHandlerResolver();

        if (resolvedEvent == null)
            return;

        await resolvedEvent(args, cancellationToken);
    }
}