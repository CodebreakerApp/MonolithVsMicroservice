using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Mapping;
using CodeBreaker.Backend.Services;
using CodeBreaker.Common.Exceptions;
using CodeBreaker.Transfer.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Backend.Endpoints;

internal static class GameTypeEndpoints
{
    public static void MapGameTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/gametypes")
            .RequireRateLimiting("default");

        group.MapGet("/", (
            [FromServices] IGameTypeService gameTypeService,
            CancellationToken cancellationToken
        ) => {
            var gameTypes = gameTypeService.GetGameTypes().ToTransfer();
            return TypedResults.Ok(new GetGameTypesResponse()
            {
                GameTypes = gameTypes 
            });
        });

        group.MapGet("/{gameTypeName:required}", Results<Ok<GetGameTypeResponse>, NotFound> (
            [FromRoute] string gameTypeName,
            [FromServices] IGameTypeService gameTypeService,
            CancellationToken cancellationToken
        ) =>
        {
            GameType gameType;

            try
            {
                gameType = gameTypeService.GetGameType(gameTypeName);
            }
            catch (NotFoundException)
            {
                return TypedResults.NotFound();
            }   

            return TypedResults.Ok(new GetGameTypeResponse() {
                GameType = gameType.ToTransfer()
            });
        });
    }
}
