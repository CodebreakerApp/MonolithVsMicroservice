using CodeBreaker.Backend.Data.DatabaseContexts;
using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Repositories.Extensions;
using CodeBreaker.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Backend.Data.Repositories;

public class EfcoreSqlGameRepository(CodeBreakerDbContext dbContext) : IGameRepository
{
    public IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default) =>
        dbContext.Games
        .WhereActive()
        .AsAsyncEnumerable();

    public async Task<Game> GetAsync(int gameId, CancellationToken cancellationToken = default) =>
        await GetCoreAsync(gameId, cancellationToken);

    public async Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync(cancellationToken);
        return game;
    }

    public async Task AddMoveAsync(int gameId, Move move, CancellationToken cancellationToken = default)
    {
        var game = await GetCoreAsync(gameId, cancellationToken);
        game.Moves.Add(move);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CancelAsync(int gameId, CancellationToken cancellationToken = default)
    {
        var game = await GetCoreAsync(gameId, cancellationToken);
        game.End = DateTime.Now;
        game.Cancelled = true;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int gameId, CancellationToken cancellationToken = default)
    {
        var game = await GetCoreAsync(gameId, cancellationToken);
        dbContext.Games.Remove(game);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(int gameId, Game game, CancellationToken cancellationToken = default)
    {
        dbContext.Games.Update(game);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async ValueTask<Game> GetCoreAsync(int gameId, CancellationToken cancellationToken = default) =>
        await dbContext.Games.FindActiveAsync(gameId, cancellationToken) ?? throw new NotFoundException($"The game with the id {gameId} was not found");
}
