using CodeBreaker.Backend.Data.Models.Bots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Backend.Data.DatabaseContexts.Configurations;

internal class BotConfiguration : IEntityTypeConfiguration<Bot>
{
    public void Configure(EntityTypeBuilder<Bot> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Game).WithMany().HasForeignKey(x => x.GameId);
    }
}

internal class SimpleBotConfiguration : IEntityTypeConfiguration<SimpleBot>
{
    public void Configure(EntityTypeBuilder<SimpleBot> builder)
    {
        builder.HasBaseType<Bot>();
    }
}
