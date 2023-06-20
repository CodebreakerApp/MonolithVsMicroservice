using CodeBreaker.Services.Bot.Runner.Mapping;
using CodeBreaker.Services.Bot.Runner.Models;
using CodeBreaker.Services.Bot.Runner.Models.Fields;
using CodeBreaker.Services.Bot.Runner.Models.Visitors;
using CodeBreaker.Services.Bot.Runner.Services.Args;
using CodeBreaker.Services.Games.Transfer.Api.Requests;
using CodeBreaker.Services.Games.Transfer.Api.Responses;
using System.Net.Http.Json;

namespace CodeBreaker.Services.Bot.Runner.Services;

internal class GameService(HttpClient httpClient) : IGameService
{
    public async Task<Game> CreateGameAsync(CreateGameArgs args, CancellationToken cancellationToken = default)
    {
        CreateGameRequest req = new()
        {
            Username = args.Username,
            GameType = args.Type.GetName(),
        };
        var result = await httpClient.PostAsJsonAsync("/games", req, cancellationToken);
        result.EnsureSuccessStatusCode();
        var resultContent = (await result.Content.ReadFromJsonAsync<CreateGameResponse>(cancellationToken)) ?? throw new InvalidOperationException("Could not deserialize the content of the HTTP-result.");
        return resultContent.Game.ToModel(args.Type);
    }
}

internal class MoveService(HttpClient httpClient) : IMoveService
{
    public async Task<Game> ApplyMoveAsync(Game game, IReadOnlyList<Field> guessPegs, CancellationToken cancellationToken = default)
    {
        CreateMoveRequest req = new()
        {
            Fields = guessPegs.ToTransfer(),
        };
        var result = await httpClient.PostAsJsonAsync($"/games/{game.Id}/moves", req, cancellationToken);
        result.EnsureSuccessStatusCode();
        var resultContent = (await result.Content.ReadFromJsonAsync<CreateMoveResponse>(cancellationToken)) ?? throw new InvalidOperationException("Could not deserialize the content of the HTTP-result.");
        Move appliedMove = new()
        {
            Fields = guessPegs,
            KeyPegs = resultContent.KeyPegs.ToModel(),
        };
        game.Moves.Add(appliedMove);
        return game;
    }
}