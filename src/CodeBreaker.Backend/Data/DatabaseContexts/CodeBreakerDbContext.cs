using CodeBreaker.Backend.Data.DatabaseContexts.Configurations;
using CodeBreaker.Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Backend.Data.DatabaseContexts;

public class CodeBreakerDbContext : DbContext
{
    public CodeBreakerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    //public DbSet<Move> Moves => Set<Move>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        //modelBuilder.ApplyConfiguration(new MoveConfiguration());
        //modelBuilder.ApplyConfiguration(new ColorFieldConfiguration());
        //modelBuilder.ApplyConfiguration(new ColorShapeFieldConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
