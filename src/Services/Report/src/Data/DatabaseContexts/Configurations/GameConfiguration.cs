using CodeBreaker.Services.Report.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeBreaker.Services.Report.Data.DatabaseContexts.Configurations;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Moves).WithOne();
        builder.Property(x => x.State).HasConversion<EnumToStringConverter<GameState>>();
        builder.HasIndex(x => x.Start);
        builder.HasIndex(x => x.End);
        builder.HasIndex(x => x.Type);
        builder.HasIndex(x => x.State);
    }
}