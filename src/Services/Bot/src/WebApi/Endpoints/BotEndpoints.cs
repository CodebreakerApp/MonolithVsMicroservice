using CodeBreaker.Services.Bot.Transfer.Api.Requests;
using CodeBreaker.Services.Bot.Transfer.Api.Responses;
using CodeBreaker.Services.Bot.WebApi.Mapping;
using CodeBreaker.Services.Bot.WebApi.Services;
using CodeBreaker.Services.Bot.WebApi.Services.Args;
using CodeBreaker.Services.Bot.WebApi.Services.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Services.Bot.WebApi.Endpoints;

internal static class BotEndpoints
{
    public static void MapBotEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/bots")
            .WithTags("Bots")
            .RequireRateLimiting("default");

        group.MapGet("/", (
            [AsParameters] GetBotsRequest req,
            [FromServices] IBotService botService,
            CancellationToken cancellationToken
        ) =>
        {
            GetBotsArgs args = new(req.From, req.To, req.MaxCount);
            var bots = botService.GetBotsAsync(args);
            return new GetBotsResponse()
            {
                Bots = bots.Select(bot => bot.ToTransfer())
            };
        })
        .WithName("GetBots")
        .WithSummary("Gets the bots.")
        .WithOpenApi();

        group.MapGet("/{id:guid}", async Task<Results<Ok<GetBotResponse>, NotFound>> (
            [FromRoute] Guid id,
            [FromServices] IBotService botService,
            CancellationToken cancellationToken
        ) =>
        {
            Data.Models.Bot bot;
            try
            {
                bot = await botService.GetBotAsync(id, cancellationToken);
            }
            catch (NotFoundException)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new GetBotResponse()
            {
                Bot = bot.ToTransfer(),
            });
        })
        .WithName("GetBot")
        .WithSummary("Gets the bot with the given id.")
        .WithOpenApi();

        group.MapPost("/", async (
            [FromBody] CreateBotRequest req,
            [FromServices] IBotService botService,
            CancellationToken cancellationToken
        ) =>
        {
            CreateBotArgs args = new(req.Name, req.BotType, req.GameType, req.ThinkTime);
            var bot = await botService.CreateAndScheduleBotAsync(args, cancellationToken);
            return TypedResults.Ok(new CreateBotResponse()
            {
                Bot = bot.ToTransfer(),
            });
        })
        .WithName("CreateBot")
        .WithSummary("Creates and schedules a bot.")
        .WithOpenApi();
    }
}
