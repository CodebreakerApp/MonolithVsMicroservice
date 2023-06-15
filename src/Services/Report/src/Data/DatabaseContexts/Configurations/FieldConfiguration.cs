using CodeBreaker.Services.Report.Data.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Services.Report.Data.DatabaseContexts.Configurations;

internal class FieldConfiguration : IEntityTypeConfiguration<Field>
{
    public void Configure(EntityTypeBuilder<Field> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.Id, x.Position }).IsUnique();
        builder.HasDiscriminator<string>("field_type");
    }
}

internal class ColorFieldConfiguration : IEntityTypeConfiguration<ColorField>
{
    public void Configure(EntityTypeBuilder<ColorField> builder)
    {
        builder.Property(x => x.Color).HasConversion<string>();
        builder.HasIndex(x => x.Color);
    }
}

internal class ColorShapeFieldConfiguration : IEntityTypeConfiguration<ColorShapeField>
{
    public void Configure(EntityTypeBuilder<ColorShapeField> builder)
    {
        builder.Property(x => x.Shape).HasConversion<string>();
        builder.HasIndex(x => x.Shape);
    }
}