using CodeBreaker.Services.Report.Data.Exceptions;
using CodeBreaker.Services.Report.Data.Models;
using CodeBreaker.Services.Report.Transfer.Api.Requests;
using CodeBreaker.Services.Report.Transfer.Api.Responses;
using CodeBreaker.Services.Report.WebApi.Mapping;
using CodeBreaker.Services.Report.WebApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Services.Report.WebApi.Endpoints;

internal static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/games")
            .WithTags("Games")
            .RequireRateLimiting("default");

        group.MapGet("/games", (
            [AsParameters] GetGamesRequest req,
            [FromServices] IGameService gameService,
            CancellationToken cancellationToken
        ) =>
        {
            GetGamesArgs args = new()
            {
                From = req.From,
                To = req.To,
                MaxCount = req.MaxCount,
            };
            return TypedResults.Ok(new GetGamesResponse()
            {
                Games = gameService.GetGamesAsync(args, cancellationToken).Select(x => x.ToTransfer())
            });
        })
        .WithName("GetGames")
        .WithSummary("Gets games within the given period of time.")
        .WithOpenApi();

        group.MapGet("/games/{id:guid}", async Task<Results<Ok<GetGameResponse>, NotFound>> (
            [FromRoute] Guid id,
            [FromServices] IGameService gameService,
            CancellationToken cancellationToken
        ) =>
        {
            GetGameArgs args = new() { Id = id };
            Game game;
            try
            {
                game = await gameService.GetGameAsync(args, cancellationToken);
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
    }
}
