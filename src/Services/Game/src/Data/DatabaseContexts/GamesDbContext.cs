using CodeBreaker.Services.Games.Data.DatabaseContexts.Configurations;
using CodeBreaker.Services.Games.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Services.Games.Data.DatabaseContexts;

public class GamesDbContext : DbContext
{
    public GamesDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
