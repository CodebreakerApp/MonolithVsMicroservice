using CodeBreaker.Frontend.Services.Options;
using CodeBreaker.Transfer;
using CodeBreaker.Transfer.Requests;
using CodeBreaker.Transfer.Responses;
using System.Net.Http.Json;

namespace CodeBreaker.Frontend.Services;

public class GameService
{
    private readonly HttpClient _httpClient;

    public GameService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetGameResponse> GetGameAsync(int gameId, CancellationToken cancellationToken)
    {
        var responseBody = await _httpClient.GetFromJsonAsync<GetGameResponse>($"/games/{gameId}", cancellationToken);
        return responseBody!;
    }

    public async Task<CreateGameResponse> StartGame(CreateGameRequest req, CancellationToken cancellationToken = default)
    {
        var res = await _httpClient.PostAsJsonAsync("/games", req, cancellationToken);
        res.EnsureSuccessStatusCode();
        var responseBody = await res.Content.ReadFromJsonAsync<CreateGameResponse>(cancellationToken);
        return responseBody!;
    }

    public async Task<CreateMoveResponse> MakeMove(int gameId, CreateMoveRequest req, CancellationToken cancellationToken = default)
    {
        var res = await _httpClient.PostAsJsonAsync($"/games/{gameId}/moves", req, cancellationToken);
        res.EnsureSuccessStatusCode();
        var responseBody = await res.Content.ReadFromJsonAsync<CreateMoveResponse>(cancellationToken);
        return responseBody!;
    }

    public async Task CancelGame(int gameId, CancellationToken cancellationToken = default)
    {
        var res = await _httpClient.DeleteAsync($"/games/{gameId}", cancellationToken);
        res.EnsureSuccessStatusCode();
    }
}
