using CodeBreaker.Services.Games.Data.Exceptions;
using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.GameLogic.Extensions;
using CodeBreaker.Services.Games.GameLogic.Visitors;
using CodeBreaker.Services.Games.Mapping;
using CodeBreaker.Services.Games.Services;
using CodeBreaker.Services.Games.Services.Exceptions;
using CodeBreaker.Services.Games.Transfer.Api.Requests;
using CodeBreaker.Services.Games.Transfer.Api.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Services.Games.Endpoints;

internal static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/games")
            .WithTags("Games");

        group.MapGet("/", (
            [FromServices] IGameService gameService,
            CancellationToken cancellationToken
        ) =>
        {
            async IAsyncEnumerable<Transfer.Api.Game> GetGamesAsync()
            {
                await foreach (var game in gameService.GetAsync(cancellationToken))
                    yield return game.ToTransfer();
            }
            return TypedResults.Ok(new GetGamesResponse()
            {
                Games = GetGamesAsync()
            });
        })
        .WithName("GetGames")
        .WithSummary("Gets the games.")
        .WithOpenApi();

        group.MapGet("/{gameId:guid}", async Task<Results<Ok<GetGameResponse>, NotFound>> (
            [FromRoute] Guid gameId,
            [FromServices] IGameService gameService,
            CancellationToken cancellationToken
        ) =>
        {
            Game game;

            try
            {
                game = await gameService.GetAsync(gameId, cancellationToken);
            }
            catch (NotFoundException)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new GetGameResponse()
            {
                Game = game.ToTransfer()
            });
        })
        .WithName("GetGame")
        .WithSummary("Gets the game with the given id.")
        .WithOpenApi();

        group.MapPost("/", async (
            [FromBody] CreateGameRequest body,
            [FromServices] IGameService gameService,
            [FromServices] IGameTypeService gameTypeService,
            CancellationToken cancellationToken
        ) =>
        {
            var game = gameTypeService
                .GetGameType(body.GameType)
                .CreateGame(new(body.Username));
            game = await gameService.CreateAsync(game, cancellationToken);
            return TypedResults.Ok(new CreateGameResponse()
            {
                Game = game.ToTransfer()
            });
        })
        .WithName("CreateGame")
        .WithSummary("Creates a game.")
        .WithOpenApi();

        group.MapDelete("/{gameId:guid}", async (
            [FromRoute] Guid gameId,
            [FromServices] IGameService gameService,
            CancellationToken cancellationToken
        ) =>
        {
            await gameService.CancelAsync(gameId, cancellationToken);
            return TypedResults.NoContent();
        })
        .WithName("CancelGame")
        .WithSummary("Cancels the game with the given id.")
        .WithOpenApi();

        group.MapPost("/{gameId:guid}/moves", async Task<Results<Ok<CreateMoveResponse>, NotFound, Conflict<string>>> (
            [FromRoute] Guid gameId,
            [FromBody] CreateMoveRequest body,
            [FromServices] IMoveService moveService,
            CancellationToken cancellationToken
        ) =>
        {
            var guessPegs = body.Fields.Select(x => x.ToModel()).ToList();
            Game game;

            try
            {
                game = await moveService.ApplyMoveAsync(gameId, guessPegs, cancellationToken);
            }
            catch (NotFoundException)
            {
                return TypedResults.NotFound();
            }
            catch (GameAlreadyEndedException)
            {
                return TypedResults.Conflict("The game has already ended");
            }

            return TypedResults.Ok(new CreateMoveResponse()
            {
                GameWon = game.State == GameState.Won,
                GameEnded = game.HasEnded(),
                KeyPegs = game.Moves.Last().KeyPegs?.ToTransfer() ?? Array.Empty<string>(),
            });
        })
        .WithName("CreateMove")
        .WithSummary("Creates and applies a move to the game with the given id.")
        .WithOpenApi();
    }
}
