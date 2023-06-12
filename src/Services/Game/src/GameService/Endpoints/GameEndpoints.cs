using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Services.Game.Endpoints;

internal static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/games")
            .WithTags("Games");

        group.MapGet("/", () => { })
        .WithName("GetGames")
        .WithSummary("Gets the games.")
        .WithOpenApi();

        group.MapGet("/{gameId:int:ming(0)", (
            [FromRoute] int gameId
        ) =>
        { })
        .WithName("GetGame")
        .WithSummary("Gets the game with the given id.")
        .WithOpenApi();

        group.MapPost("/", () => { })
        .WithName("CreateGame")
        .WithSummary("Creates a game.")
        .WithOpenApi();

        group.MapDelete("/{gameId:int:min(0)", (
            [FromRoute] int gameId
        ) =>
        { })
        .WithName("CancelGame")
        .WithSummary("Cancels the game with the given id.")
        .WithOpenApi();

        group.MapPost("/{gameId:int:min(0)}/moves", (
            [FromRoute] int gameId
        ) =>
        { })
        .WithName("CreateMove")
        .WithSummary("Creates and applies a move to the game with the given id.")
        .WithOpenApi();
    }
}
