using CodeBreaker.Services.Games.Data.DatabaseContexts;
using CodeBreaker.Services.Games.Data.Exceptions;
using CodeBreaker.Services.Games.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Services.Games.Data.Repositories;

public class GameRepository(GamesDbContext dbContext) : IGameRepository
{
    public IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default) =>
        dbContext.Games
        .Include(game => game.Moves.OrderBy(move => move.CreatedAt))
        .AsAsyncEnumerable();

    public async Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default) =>
        await GetCoreAsync(gameId, cancellationToken);

    public async Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync(cancellationToken);
        return game;
    }

    public async Task AddMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken = default)
    {
        var game = await GetCoreAsync(gameId, cancellationToken);
        game.Moves.Add(move);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CancelAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        var game = await GetCoreAsync(gameId, cancellationToken);
        game.End = DateTime.Now;
        game.State = GameState.Cancelled;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        var game = await GetCoreAsync(gameId, cancellationToken);
        dbContext.Games.Remove(game);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid gameId, Game game, CancellationToken cancellationToken = default)
    {
        dbContext.Games.Update(game);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<Game> GetCoreAsync(Guid gameId, CancellationToken cancellationToken = default) =>
        await dbContext.Games
        .Include(game => game.Moves.OrderBy(move => move.CreatedAt))
        .SingleOrDefaultAsync(game => game.Id == gameId, cancellationToken) ?? throw new NotFoundException($"The game with the id {gameId} was not found");
}
