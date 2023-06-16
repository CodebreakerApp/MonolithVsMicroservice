﻿using CodeBreaker.Services.Games.Data.DatabaseContexts.Converters;
using CodeBreaker.Services.Games.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeBreaker.Services.Games.Data.DatabaseContexts.Configurations;

internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type).HasConversion<GameTypeNameConverter>();
        builder.Property(x => x.Code).HasConversion<FieldsConverter>();
        builder.OwnsMany(x => x.Moves, builder =>
        {
            builder.ToTable("Moves");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Fields).HasConversion<FieldsConverter>();
            builder.Property(x => x.KeyPegs).HasConversion<KeyPegsConverter>();
        });
        builder.Property(x => x.State).HasConversion<EnumToStringConverter<GameState>>();
        builder.HasIndex(x => x.Start);
        builder.HasIndex(x => x.End);
        builder.HasIndex(x => x.Type);
    }
}