using CodeBreaker.Services.Report.Data.DatabaseContexts.Configurations;
using CodeBreaker.Services.Report.Data.Models;
using CodeBreaker.Services.Report.Data.Models.Fields;
using CodeBreaker.Services.Report.Data.Models.KeyPegs;
using Microsoft.EntityFrameworkCore;

namespace CodeBreaker.Services.Report.Data.DatabaseContexts;

public class ReportDbContext : DbContext
{
    public ReportDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Move> Moves => Set<Move>();

    public DbSet<Field> Fields => Set<Field>();

    public DbSet<ColorField> ColorFields => Set<ColorField>();

    public DbSet<ColorShapeField> ColorShapeFields => Set<ColorShapeField>();

    public DbSet<KeyPeg> KeyPegs => Set<KeyPeg>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new MoveConfiguration());
        modelBuilder.ApplyConfiguration(new FieldConfiguration());
        modelBuilder.ApplyConfiguration(new ColorShapeFieldConfiguration());
        modelBuilder.ApplyConfiguration(new ColorFieldConfiguration());
        modelBuilder.ApplyConfiguration(new KeyPegConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
