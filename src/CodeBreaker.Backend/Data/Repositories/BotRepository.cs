using CodeBreaker.Backend.Data.DatabaseContexts;
using CodeBreaker.Backend.Data.Models.Bots;

namespace CodeBreaker.Backend.Data.Repositories;

public class BotRepository(CodeBreakerDbContext dbContext) : IBotRepository
{
    public async Task<Bot> CreateAsync(Bot bot, CancellationToken cancellationToken = default)
    {
        dbContext.Bots.Add(bot);
        await dbContext.SaveChangesAsync(cancellationToken);
        return bot;
    }
}
