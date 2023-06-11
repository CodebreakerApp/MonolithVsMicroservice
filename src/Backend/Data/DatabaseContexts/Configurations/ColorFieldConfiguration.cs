using CodeBreaker.Backend.Data.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Backend.Data.DatabaseContexts.Configurations;

internal class ColorFieldConfiguration : IEntityTypeConfiguration<ColorField>
{
    public void Configure(EntityTypeBuilder<ColorField> builder)
    {
        builder.HasNoKey();
    }
}
