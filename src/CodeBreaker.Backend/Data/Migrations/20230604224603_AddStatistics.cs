using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreaker.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
CREATE OR ALTER FUNCTION QueryStatistics(@from DATETIME2, @to DATETIME2, @gameType NVARCHAR)
RETURNS TABLE
AS
RETURN
(
    SELECT
    COUNT([Games].[Id])                                                                                                                             AS TotalGames,
    (SELECT COUNT([Id]) FROM [Games] WHERE [Start] >= @from AND [Start] < @to AND ([Type] = @gameType OR @gameType IS NULL) AND [Won] = 1)          AS WonGames,
    (SELECT COUNT([Id]) FROM [Games] WHERE [Start] >= @from AND [Start] < @to AND ([Type] = @gameType OR @gameType IS NULL) AND [Cancelled] = 1)    AS CancelledGames,
    ISNULL(MIN(DATEDIFF(MILLISECOND, [Games].[Start], [Games].[End])), 0)                                                                           AS MinGameDuration,
    ISNULL(AVG(DATEDIFF(MILLISECOND, [Games].[Start], [Games].[End])), 0)                                                                           AS AvgGameDuration,
    ISNULL(MAX(DATEDIFF(MILLISECOND, [Games].[Start], [Games].[End])), 0)                                                                           AS MaxGameDuration,
    ISNULL(MIN([MoveCounts].[MoveCount]), 0)                                                                                                        AS MinMoveCount,
    ISNULL(MAX([MoveCounts].[MoveCount]), 0)                                                                                                        AS MaxMoveCount,
    ISNULL(AVG([MoveCounts].[MoveCount]), 0)                                                                                                        AS AvgMoveCount
    FROM Games
    LEFT JOIN
    (
        SELECT
            [Games].[Id]        AS GameId,
            COUNT([Moves].[Id]) AS MoveCount
        FROM [Games]
        INNER JOIN [Moves] ON [Moves].[GameId] = [Games].[Id]
        WHERE [Start] >= @from AND [Start] < @to AND ([Type] = @gameType OR @gameType IS NULL)
        GROUP BY [Games].Id
    ) AS MoveCounts ON MoveCounts.GameId = Games.Id
    WHERE [Start] >= @from AND [Start] < @to AND ([Type] = @gameType OR @gameType IS NULL)
);
""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION [QueryStatistics]");
        }
    }
}
