using CodeBreaker.Services.Bot.Data.DatabaseContexts.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Services.Bot.Data.DatabaseContexts;

public class BotDbContext : DbContext
{
    public BotDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Models.Bot> Bots => Set<Models.Bot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BotConfiguration());
        modelBuilder.ApplyConfiguration(new SimpleBotConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
