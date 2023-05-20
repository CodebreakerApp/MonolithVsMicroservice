using CodeBreaker.Backend.Data.DatabaseContexts.Converters;
using CodeBreaker.Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Backend.Data.DatabaseContexts.Configurations;

internal class MoveConfiguration : IEntityTypeConfiguration<Move>
{
    public void Configure(EntityTypeBuilder<Move> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsMany(x => x.Fields).ToJson();
        builder.Property(x => x.KeyPegs).HasConversion(new KeyPegsConverter());
    }
}
