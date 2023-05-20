using CodeBreaker.Backend.Data.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Backend.Data.DatabaseContexts.Configurations;

internal class ColorShapeFieldConfiguration : IEntityTypeConfiguration<ColorShapeField>
{
    public void Configure(EntityTypeBuilder<ColorShapeField> builder)
    {
        builder.HasNoKey();
    }
}
