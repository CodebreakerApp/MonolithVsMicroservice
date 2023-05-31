using CodeBreaker.Backend.Data.DatabaseContexts.Converters;
using CodeBreaker.Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Backend.Data.DatabaseContexts.Configurations;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type).HasConversion<GameTypeNameConverter>(); // Store gameType by name
        builder.Property(x => x.Code).HasConversion<FieldsConverter>();
        builder.OwnsMany(x => x.Moves, builder =>
        {
            builder.ToTable("Moves");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Fields).HasConversion<FieldsConverter>();
            builder.Property(x => x.KeyPegs).HasConversion<KeyPegsConverter>();
        });
    }
}
