using CodeBreaker.Backend.BotLogic;
using CodeBreaker.Transfer.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Backend.Endpoints;

internal static class BotTypeEndpoints
{
    public static void MapBotTypeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/bottypes")
            .RequireRateLimiting("default");

        group.MapGet("/", ([FromServices] IBotFactory botFactory) =>
        {
            var botTypes = botFactory.AvailableBotNames;
            return TypedResults.Ok(new GetBotTypesResponse()
            {
                BotTypeNames = botTypes
            });
        });
    }
}
