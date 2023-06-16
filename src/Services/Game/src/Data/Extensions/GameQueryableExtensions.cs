using CodeBreaker.Services.Games.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Services.Games.Data.Extensions;

internal static class GameQueryableExtensions
{
    public static IQueryable<Game> WhereActive(this IQueryable<Game> games) =>
        games.Where(game =>
            game.State == GameState.Active &&                             // active
            game.Start >= DateTime.Now.AddDays(-1)    // not orphaned (1 day)
        );

    public static IQueryable<Game> WhereNotActive(this IQueryable<Game> games) =>
        games.Where(game =>
            game.State != GameState.Active ||                             // not active
            game.Start < DateTime.Now.AddDays(-1)     // orphaned (1 day)
        );

    public static Task<Game?> FindActiveAsync(this IQueryable<Game> games, Guid id, CancellationToken cancellationToken = default) =>
        games.WhereActive().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
}
