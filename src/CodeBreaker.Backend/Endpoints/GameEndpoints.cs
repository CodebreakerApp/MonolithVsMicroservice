using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.GameLogic.Extensions;
using CodeBreaker.Backend.Mapping;
using CodeBreaker.Backend.Services;
using CodeBreaker.Common.Exceptions;
using CodeBreaker.Transfer.Requests;
using CodeBreaker.Transfer.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Transfer = CodeBreaker.Transfer;

namespace Microsoft.AspNetCore.Routing;

internal static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/games")
            .RequireRateLimiting("default");

        group.MapGet("/", (
            [FromServices] IGameService gameService,
            CancellationToken cancellationToken
        ) =>
        {
            async IAsyncEnumerable<Transfer.Game> GetGamesAsync()
            {
                await foreach (var game in gameService.GetAsync(cancellationToken))
                    yield return game.ToTransfer();
            }
            return TypedResults.Ok(new GetGamesResponse()
            {
                Games = GetGamesAsync()
            });
        });

        group.MapGet("/{id:int:min(0)}", async Task<Results<Ok<GetGameResponse>, NotFound>> (
            [FromRoute] int id,
            [FromServices] IGameService gameService,
            CancellationToken cancellationToken
        ) =>
        {
            Game game;

            try
            {
                game = await gameService.GetAsync(id, cancellationToken);
            }
            catch (NotFoundException)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new GetGameResponse()
            {
                Game = game.ToTransfer()
            });
        });

        group.MapPost("/", async (
            [FromBody] CreateGameRequest body,
            [FromServices] IGameService gameService,
            [FromServices] IGameTypeService gameTypeService,
            CancellationToken cancellationToken
        ) =>
        {
            var game = gameTypeService
                .GetGameType(body.GameType)
                .CreateGame(new (body.Username));
            game = await gameService.CreateAsync(game, cancellationToken);
            return TypedResults.Ok(new CreateGameResponse()
            {
                Game = game.ToTransfer()
            });
        });

        group.MapDelete("/{id:int:min(0)}", async (
            [FromRoute] int id,
            [FromServices] IGameService gameService,
            CancellationToken cancellationToken
        ) =>
        {
            await gameService.CancelAsync(id, cancellationToken);
            return TypedResults.NoContent();
        });

        group.MapPost("/{gameId:int:min(0)}/moves", async Task<Results<Ok<CreateMoveResponse>, NotFound, BadRequest<string>>> (
            [FromRoute] int gameId,
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
                return TypedResults.BadRequest("The game has already ended");
            }

            return TypedResults.Ok(new CreateMoveResponse()
            {
                GameWon = game.Won,
                GameEnded = game.HasEnded(),
                KeyPegs = game.Moves.Last().KeyPegs?.ToTransfer() ?? Array.Empty<string>(),
            });
        });
    }
}
