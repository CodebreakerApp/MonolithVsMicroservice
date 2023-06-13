﻿using CodeBreaker.Services.Games.Data.DatabaseContexts;
using CodeBreaker.Services.Games.Data.Exceptions;
using CodeBreaker.Services.Games.Data.Models;

namespace CodeBreaker.Services.Games.Data.Repositories;

public class GameRepository(GamesDbContext dbContext) : IGameRepository
{
    public IAsyncEnumerable<Game> GetAsync(CancellationToken cancellationToken = default) =>
        dbContext.Games.AsAsyncEnumerable();

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
        await dbContext.Games.FindAsync(gameId, cancellationToken) ?? throw new NotFoundException($"The game with the id {gameId} was not found");
}
