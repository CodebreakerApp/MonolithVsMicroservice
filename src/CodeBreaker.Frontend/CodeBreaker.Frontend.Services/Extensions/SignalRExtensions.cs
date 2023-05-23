namespace Microsoft.AspNetCore.SignalR.Client;

internal static class SignalRExtensions
{
    public static IHubConnectionBuilder WithAutomaticReconnect(this IHubConnectionBuilder builder, bool automaticReconnect) =>
        automaticReconnect
            ? builder.WithAutomaticReconnect()
            : builder;
}
