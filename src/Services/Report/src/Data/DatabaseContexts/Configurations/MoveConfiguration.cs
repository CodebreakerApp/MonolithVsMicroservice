using CodeBreaker.Services.Report.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Services.Report.Data.DatabaseContexts.Configurations;

internal class MoveConfiguration : IEntityTypeConfiguration<Move>
{
    public void Configure(EntityTypeBuilder<Move> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.Id, x.Position }).IsUnique();
        builder.HasMany(x => x.Fields);
        builder.HasMany(x => x.KeyPegs);
    }
}
