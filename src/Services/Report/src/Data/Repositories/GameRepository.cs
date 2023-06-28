﻿using CodeBreaker.Services.Report.Data.Exceptions;
using CodeBreaker.Services.Report.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using CodeBreaker.Services.Report.Data.Models;
using CodeBreaker.Services.Report.Data.Models.Fields;
using CodeBreaker.Services.Report.Data.Models.KeyPegs;
using CodeBreaker.Services.Report.Data.Repositories.Args;

namespace CodeBreaker.Services.Report.Data.Repositories;

public class GameRepository(ReportDbContext dbContext) : IGameRepository, IDisposable
{
    public IAsyncEnumerable<Game> GetAsync(GetGamesArgs args, CancellationToken cancellationToken = default) =>
        dbContext.Games
            .Include(x => x.Code)
            .Include(game => game.Moves.OrderBy(move => move.CreatedAt))
            .ThenInclude(move => move.Fields.OrderBy(field => field.Position))
            .Include(game => game.Moves.OrderBy(move => move.CreatedAt))
            .ThenInclude(move => move.KeyPegs!.OrderBy(keyPeg => keyPeg.Position))
            .Where(x => x.Start >= args.From && x.Start < args.To)
            .Take(args.MaxCount)
            .AsAsyncEnumerable();

    public async Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default) =>
        await GetCoreAsync(gameId, cancellationToken);

    public async Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        SyncPositions(game);
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync(cancellationToken);
        return game;
    }

    public async Task DeleteAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        var game = await GetCoreAsync(gameId, cancellationToken);
        dbContext.Games.Remove(game);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid gameId, Game game, CancellationToken cancellationToken = default)
    {
        SyncPositions(game);
        dbContext.Games.Update(game);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken = default)
    {
        move.GameId = gameId;
        dbContext.Moves.Add(move);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<Game> GetCoreAsync(Guid gameId, CancellationToken cancellationToken = default) =>
        await dbContext.Games
            .Include(x => x.Code.OrderBy(x => x.Position))
            .Include(x => x.Moves.OrderBy(x => x.CreatedAt))
            .ThenInclude(move => move.Fields.OrderBy(field => field.Position))
            .Include(game => game.Moves.OrderBy(move => move.CreatedAt))
            .ThenInclude(move => move.KeyPegs!.OrderBy(keyPeg => keyPeg.Position))
            .Where(x => x.Id == gameId)
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException($"The game with the id {gameId} was not found");

    private static void SyncPositions(Game game)
    {
        SyncPositions(game.Code);
        SyncPositions(game.Moves);
    }

    private static void SyncPositions(IEnumerable<Move> moves)
    {
        foreach (var move in moves)
        {
            SyncPositions(move.Fields);
            if (move.KeyPegs != null)
                SyncPositions(move.KeyPegs);
        }
    }

    private static void SyncPositions(IEnumerable<Field> fields)
    {
        int i = 0;
        foreach (var field in fields)
            field.Position = i++;
    }

    private static void SyncPositions(IEnumerable<KeyPeg> keyPegs)
    {
        int i = 0;
        foreach (var keyPeg in keyPegs)
            keyPeg.Position = i++;
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}
