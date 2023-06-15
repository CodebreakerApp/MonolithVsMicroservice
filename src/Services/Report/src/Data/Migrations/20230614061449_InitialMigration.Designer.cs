﻿// <auto-generated />
using System;
using CodeBreaker.Services.Report.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodeBreaker.Services.Report.Data.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    [Migration("20230614061449_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.4.23259.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Fields.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<int?>("MoveId")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("field_type")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("MoveId");

                    b.HasIndex("Id", "Position")
                        .IsUnique();

                    b.ToTable("Fields");

                    b.HasDiscriminator<string>("field_type").HasValue("Field");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Won")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("End");

                    b.HasIndex("Start");

                    b.HasIndex("Type");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.KeyPegs.KeyPeg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("MoveId")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Color");

                    b.HasIndex("MoveId");

                    b.HasIndex("Id", "Position")
                        .IsUnique();

                    b.ToTable("KeyPegs");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("Id", "Position")
                        .IsUnique();

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Fields.ColorField", b =>
                {
                    b.HasBaseType("CodeBreaker.Services.Report.Data.Models.Fields.Field");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("Color");

                    b.HasDiscriminator().HasValue("ColorField");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Fields.ColorShapeField", b =>
                {
                    b.HasBaseType("CodeBreaker.Services.Report.Data.Models.Fields.ColorField");

                    b.Property<string>("Shape")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("Shape");

                    b.HasDiscriminator().HasValue("ColorShapeField");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Fields.Field", b =>
                {
                    b.HasOne("CodeBreaker.Services.Report.Data.Models.Game", null)
                        .WithMany("Code")
                        .HasForeignKey("GameId");

                    b.HasOne("CodeBreaker.Services.Report.Data.Models.Move", null)
                        .WithMany("Fields")
                        .HasForeignKey("MoveId");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.KeyPegs.KeyPeg", b =>
                {
                    b.HasOne("CodeBreaker.Services.Report.Data.Models.Move", null)
                        .WithMany("KeyPegs")
                        .HasForeignKey("MoveId");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Move", b =>
                {
                    b.HasOne("CodeBreaker.Services.Report.Data.Models.Game", null)
                        .WithMany("Moves")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Game", b =>
                {
                    b.Navigation("Code");

                    b.Navigation("Moves");
                });

            modelBuilder.Entity("CodeBreaker.Services.Report.Data.Models.Move", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("KeyPegs");
                });
#pragma warning restore 612, 618
        }
    }
}
