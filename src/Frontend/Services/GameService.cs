using CodeBreaker.Services.Games.Transfer.Api.Requests;
using CodeBreaker.Services.Games.Transfer.Api.Responses;
using System.Net.Http.Json;

namespace CodeBreaker.Frontend.Services;

public class GameService(HttpClient httpClient)
{
    public async Task<GetGameResponse> GetGameAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        var responseBody = await httpClient.GetFromJsonAsync<GetGameResponse>($"/games/{gameId}", cancellationToken);
        return responseBody ?? throw new InvalidOperationException();
    }

    public async Task<CreateGameResponse> StartGame(CreateGameRequest req, CancellationToken cancellationToken = default)
    {
        var res = await httpClient.PostAsJsonAsync("/games", req, cancellationToken);
        res.EnsureSuccessStatusCode();
        var responseBody = await res.Content.ReadFromJsonAsync<CreateGameResponse>(cancellationToken);
        return responseBody ?? throw new InvalidOperationException();
    }

    public async Task<CreateMoveResponse> MakeMove(Guid gameId, CreateMoveRequest req, CancellationToken cancellationToken = default)
    {
        var res = await httpClient.PostAsJsonAsync($"/games/{gameId}/moves", req, cancellationToken);
        res.EnsureSuccessStatusCode();
        var responseBody = await res.Content.ReadFromJsonAsync<CreateMoveResponse>(cancellationToken);
        return responseBody ?? throw new InvalidOperationException();
    }

    public async Task CancelGame(int gameId, CancellationToken cancellationToken = default)
    {
        var res = await httpClient.DeleteAsync($"/games/{gameId}", cancellationToken);
        res.EnsureSuccessStatusCode();
    }
}
