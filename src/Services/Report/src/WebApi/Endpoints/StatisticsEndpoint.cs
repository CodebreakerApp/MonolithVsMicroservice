using CodeBreaker.Services.Report.Mapping;
using CodeBreaker.Services.Report.Transfer.Api.Requests;
using CodeBreaker.Services.Report.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Services.Report.WebApi.Endpoints;

internal static class StatisticsEndpoint
{
    public static void MapStatisticsEndpoint(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("/statistics")
            .WithTags("Statistics")
            .RequireRateLimiting("default");

        group.MapGet("/", async (
            [AsParameters] GetStatisticsRequest req,
            [FromServices] IStatisticsService statisticsService,
            CancellationToken cancellationToken
        ) =>
        {
            GetStatisticsArgs args = new()
            {
                From = req.From,
                To = req.To,
                GameType = req.GameType,
            };
            var statistics = await statisticsService.GetStatisticsAsync(args, cancellationToken);
            return TypedResults.Ok(statistics.ToTransfer());
        })
        .WithName("GetStatistics")
        .WithSummary("Gets statistics for the games within the given period of time.")
        .WithOpenApi();
    }
}
