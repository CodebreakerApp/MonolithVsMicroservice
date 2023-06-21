using CodeBreaker.Services.Bot.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBreaker.Services.Bot.Data.DatabaseContexts.Configurations;

internal class BotConfiguration : IEntityTypeConfiguration<Models.Bot>
{
    public void Configure(EntityTypeBuilder<Models.Bot> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.State).HasConversion<string>();
    }
}

internal class SimpleBotConfiguration : IEntityTypeConfiguration<SimpleBot>
{
    public void Configure(EntityTypeBuilder<SimpleBot> builder)
    {
        builder.HasBaseType<Models.Bot>().HasDiscriminator<string>("BotType");
    }
}
