using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Repositories;
using CodeBreaker.Backend.Mapping;
using CodeBreaker.Backend.Services;
using CodeBreaker.Common.Exceptions;
using CodeBreaker.Transfer.Requests;
using CodeBreaker.Transfer.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Backend.Endpoints;

internal static class ReportEndpoints
{
    public static void MapReportEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/reports")
            .RequireRateLimiting("default");

        group.MapGet("/statistics", async (
            [AsParameters] GetStatisticsRequest req,
            [FromServices] IReportService reportService,
            [FromServices] IGameTypeRepository gameTypeRepository,
            CancellationToken cancellationToken
        ) =>
        {
            Services.GetStatisticsArgs args = new()
            {
                From = req.From,
                To = req.To,
                GameType = !string.IsNullOrWhiteSpace(req.GameType) ? gameTypeRepository.GetGameType(req.GameType) : null
            };
            var statistics = await reportService.GetStatisticsAsync(args, cancellationToken);
            return TypedResults.Ok(statistics.ToTransfer());
        });

        group.MapGet("/games", (
            [AsParameters] GetReportGamesRequest req,
            [FromServices] IReportService reportService,
            CancellationToken cancellationToken
        ) =>
        {
            Services.GetGamesArgs args = new()
            {
                From = req.From,
                To = req.To,
                MaxCount = req.MaxCount
            };
            return TypedResults.Ok(new GetReportGamesResponse()
            {
                Games = reportService.GetGamesAsync(args).Select(x => x.ToTransferWithCode())
            });
        });

        group.MapGet("/games/{id:int:min(0)}", async Task<Results<Ok<GetReportGameResponse>, NotFound>> (
            [FromRoute] int id,
            [FromServices] IReportService reportService,
            CancellationToken cancellationToken
        ) =>
        {
            Services.GetGameArgs args = new()
            {
                Id = id
            };

            Game game;
            try
            {
                game = await reportService.GetGameAsync(args, cancellationToken);
            }
            catch (NotFoundException)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new GetReportGameResponse()
            {
                Game = game.ToTransferWithCode()
            });
        });
    }
}
