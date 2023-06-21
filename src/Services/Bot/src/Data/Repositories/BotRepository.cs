﻿using CodeBreaker.Services.Bot.Data.DatabaseContexts;
using CodeBreaker.Services.Bot.Data.Exceptions;
using CodeBreaker.Services.Bot.Data.Models;
using CodeBreaker.Services.Bot.Data.Models.Extensions;
using CodeBreaker.Services.Bot.Data.Repositories.Args;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CodeBreaker.Services.Bot.Data.Repositories;

public class BotRepository(BotDbContext dbContext) : IBotRepository
{
    public IAsyncEnumerable<Models.Bot> GetBotsAsync(GetBotsArgs args) =>
        dbContext.Bots
            .Where(x => x.CreatedAt >= args.From && x.CreatedAt < args.To)
            .Take(args.MaxCount)
            .AsAsyncEnumerable();

    public async Task<Models.Bot> GetBotAsync(Guid id, CancellationToken cancellationToken) =>
        await dbContext.Bots.FindAsync(id.ToString(), cancellationToken) ?? throw new NotFoundException($"The bot with the id {id} was not found");

    public async Task<Models.Bot> CreateAsync(Models.Bot bot, CancellationToken cancellationToken = default)
    {
        dbContext.Bots.Add(bot);
        await dbContext.SaveChangesAsync(cancellationToken);
        return bot;
    }

    public async Task UpdateGameIdAsync(Guid botId, Guid gameId, CancellationToken cancellationToken = default) =>
        await dbContext.Bots
            .Where(x => x.Id == botId)
            .ExecuteUpdateAsync(x => x.SetProperty(bot => bot.GameId, gameId), cancellationToken);

    public async Task UpdateStateAsync(Guid id, BotState newState, CancellationToken cancellationToken = default)
    {
        var queryable = dbContext.Bots.Where(x => x.Id == id);

        if (newState.HasEnded())
            await queryable.ExecuteUpdateAsync(x => x
                    .SetProperty(bot => bot.State, newState)
                    .SetProperty(bot => bot.EndedAt, DateTime.Now), cancellationToken);
        else
            await queryable.ExecuteUpdateAsync(x => x.SetProperty(bot => bot.State, newState), cancellationToken);
    }
}