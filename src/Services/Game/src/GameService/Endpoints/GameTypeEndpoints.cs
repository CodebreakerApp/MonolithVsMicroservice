using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Services.Game.Endpoints;

internal static class GameTypeEndpoints
{
    public static void MapGameTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/gametypes")
            .WithTags("GameTypes");

        group.MapGet("/", () => { })
        .WithName("GetGameTypes")
        .WithSummary("Gets the available game types.")
        .WithOpenApi();

        group.MapGet("/{gameTypeName:required", (
            [FromRoute] string gameTypeName
        ) =>
        { })
        .WithName("GetGameType")
        .WithSummary("Gets the game type with the specified name.")
        .WithOpenApi();
    }
}
