using CodeBreaker.Services.Games.Data.Exceptions;
using CodeBreaker.Services.Games.Data.Models.GameTypes;
using CodeBreaker.Services.Games.Mapping;
using CodeBreaker.Services.Games.Services;
using CodeBreaker.Services.Games.Transfer.Api.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Services.Games.Endpoints;

internal static class GameTypeEndpoints
{
    public static void MapGameTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/gametypes")
            .WithTags("GameTypes");

        group.MapGet("/", (
            [FromServices] IGameTypeService gameTypeService,
            CancellationToken cancellationToken
        ) => {
            var gameTypes = gameTypeService.GetGameTypes().ToTransfer();
            return TypedResults.Ok(new GetGameTypesResponse()
            {
                GameTypes = gameTypes
            });
        })
        .WithName("GetGameTypes")
        .WithSummary("Gets the available game types.")
        .WithOpenApi();

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

            return TypedResults.Ok(new GetGameTypeResponse()
            {
                GameType = gameType.ToTransfer()
            });
        })
        .WithName("GetGameType")
        .WithSummary("Gets the game type with the specified name.")
        .WithOpenApi();
    }
}
