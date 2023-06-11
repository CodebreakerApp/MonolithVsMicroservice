using CodeBreaker.Backend.Mapping;
using CodeBreaker.Backend.Services;
using CodeBreaker.Transfer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Backend.Endpoints;

internal static class BotEndpoints
{
    public static void MapBotEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/bots")
            .WithTags("Bots")
            .RequireRateLimiting("default");

        group.MapPost("/", async (
            [FromBody] CreateBotRequest body,
            [FromServices] IBotService botService,
            [FromServices] IGameTypeService gameTypeService,
            CancellationToken cancellationToken
        ) =>
        {
            var gameType = gameTypeService.GetGameType(body.GameType);
            CreateBotArgs args = new(body.BotName, gameType, body.ThinkTime);
            var bot = await botService.CreateBotAsync(args, cancellationToken);
            return TypedResults.Ok(bot.ToTransfer());
        })
        .WithName("CreateBot")
        .WithSummary("Creates a bot and schedules its run.")
        .WithOpenApi();
    }
}
