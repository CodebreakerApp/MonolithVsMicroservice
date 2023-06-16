using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreaker.Services.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
CREATE OR ALTER FUNCTION QueryCount(@from DATETIME2, @to DATETIME2, @gameType NVARCHAR(450), @gameState NVARCHAR(MAX))
RETURNS INT
AS
BEGIN
    DECLARE @return INT;
    SELECT @return = COUNT([Id]) FROM [Games]
    WHERE
    [Start] >= @from AND                                                -- within period of time
    [Start] < @to AND                                                   -- within period of time
    ([Type] = @gameType OR @gameType IS NULL) AND                       -- matching gametype
    [State] = @gameState;                                               -- matching gamestate

    RETURN @return
END;
""");
            migrationBuilder.Sql("""
CREATE OR ALTER FUNCTION QueryStatistics(@from DATETIME2, @to DATETIME2, @gameType NVARCHAR(450))
RETURNS TABLE
AS
RETURN
(
    SELECT
        COUNT([Games].[Id])                                                         AS TotalGames,
        dbo.QueryCount(@from, @to, @gameType, 'Active')                             AS ActiveGames,
        dbo.QueryCount(@from, @to, @gameType, 'Won')                                AS WonGames,
        dbo.QueryCount(@from, @to, @gameType, 'Lost')                               AS LostGames,
        dbo.QueryCount(@from, @to, @gameType, 'Cancelled')                          AS CancelledGames,
        dbo.QueryCount(@from, @to, @gameType, 'Orphaned')                          AS OrphanedGames,
        ISNULL(MIN(DATEDIFF(MILLISECOND, [Games].[Start], [Games].[End])), 0)       AS MinGameDuration,
        ISNULL(AVG(DATEDIFF(MILLISECOND, [Games].[Start], [Games].[End])), 0)       AS AvgGameDuration,
        ISNULL(MAX(DATEDIFF(MILLISECOND, [Games].[Start], [Games].[End])), 0)       AS MaxGameDuration,
        ISNULL(MIN([MoveCounts].[MoveCount]), 0)                                    AS MinMoveCount,
        ISNULL(MAX([MoveCounts].[MoveCount]), 0)                                    AS MaxMoveCount,
        ISNULL(AVG([MoveCounts].[MoveCount]), 0)                                    AS AvgMoveCount
        FROM Games
        LEFT JOIN
        (
            -- MoveCounts
            SELECT
                [Games].[Id]        AS GameId,
                COUNT([Moves].[Id]) AS MoveCount
            FROM [Games]
            INNER JOIN [Moves] ON [Moves].[GameId] = [Games].[Id]
            WHERE
                [Start] >= @from AND                                                -- within period of time
                [Start] < @to AND                                                   -- within period of time
                ([Type] = @gameType OR @gameType IS NULL)                           -- matching gametype
            GROUP BY [Games].Id
        ) AS MoveCounts ON MoveCounts.GameId = Games.Id
        WHERE
            [Start] >= @from AND                                                -- within period of time
            [Start] < @to AND                                                   -- within period of time
            ([Type] = @gameType OR @gameType IS NULL)                           -- matching gametype
);
""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION dbo.QueryStatistics");
            migrationBuilder.Sql("DROP FUNCTION dbo.QueryCount");
        }
    }
}
