using CodeBreaker.Services.Report.Data.Models.KeyPegs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Services.Report.Data.DatabaseContexts.Configurations;

internal class KeyPegConfiguration : IEntityTypeConfiguration<KeyPeg>
{
    public void Configure(EntityTypeBuilder<KeyPeg> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.Id, x.Position }).IsUnique();
        builder.HasIndex(x => x.Color);
        builder.Property(x => x.Color).HasConversion<string>();
    }
}
