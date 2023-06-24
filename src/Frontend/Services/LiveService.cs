using CodeBreaker.Frontend.Services.Options;
using CodeBreaker.Services.Live.Transfer;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace CodeBreaker.Frontend.Services;

public class LiveService
{
    private readonly HubConnection _hubConnection;

    public event EventHandler<GameCreatedPayload>? OnGameCreated;

    public event EventHandler<GameEndedPayload>? OnGameEnded;

    public event EventHandler<MoveCreatedPayload>? OnMoveMade;

    public LiveService(IOptions<LiveServiceOptions> options)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(options.Value.Url)
            .WithAutomaticReconnect(options.Value.AutomaticReconnect)
            .Build();
        _hubConnection.On<GameCreatedPayload>("GameCreated", payload => OnGameCreated?.Invoke(this, payload));
        _hubConnection.On<GameEndedPayload>("GameEnded", payload => OnGameEnded?.Invoke(this, payload));
        _hubConnection.On<MoveCreatedPayload>("MoveMade", payload => OnMoveMade?.Invoke(this, payload));
    }

    public Task ConnectAsync(CancellationToken cancellationToken = default) =>
        _hubConnection.StartAsync(cancellationToken);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) =>
        _hubConnection.StopAsync(cancellationToken);
}
