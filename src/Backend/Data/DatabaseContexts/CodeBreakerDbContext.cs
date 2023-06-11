using CodeBreaker.Backend.Data.DatabaseContexts.Configurations;
using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Bots;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Backend.Data.DatabaseContexts;

public class CodeBreakerDbContext : DbContext
{
    public CodeBreakerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Bot> Bots => Set<Bot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new BotConfiguration());
        modelBuilder.ApplyConfiguration(new SimpleBotConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
