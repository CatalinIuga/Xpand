﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xpand.Api.Data;

#nullable disable

namespace Xpand.Api.Migrations
{
    [DbContext(typeof(XpandDbContext))]
    [Migration("20231129085329_LastMigration")]
    partial class LastMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Xpand.Api.Models.Captain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId")
                        .IsUnique()
                        .HasFilter("[TeamId] IS NOT NULL");

                    b.ToTable("Captains");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Yoda",
                            Password = "4213",
                            TeamId = 1
                        });
                });

            modelBuilder.Entity("Xpand.Api.Models.Planet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RobotsCount")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Planets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "On this planet we live.",
                            ImageName = "earth.png",
                            Name = "Earth",
                            RobotsCount = 2,
                            Status = "OK",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "This one planet we can live.",
                            ImageName = "mars.png",
                            Name = "Mars",
                            RobotsCount = 2,
                            Status = "OK",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "No description yet :/",
                            ImageName = "jupiter.png",
                            Name = "Jupiter",
                            RobotsCount = 0,
                            Status = "TODO"
                        },
                        new
                        {
                            Id = 4,
                            Description = "This one planet we can't live.",
                            ImageName = "saturn.png",
                            Name = "Saturn",
                            RobotsCount = 2,
                            Status = "NotOK",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 5,
                            Description = "No description yet :/",
                            ImageName = "uranus.png",
                            Name = "Uranus",
                            RobotsCount = 0,
                            Status = "TODO"
                        },
                        new
                        {
                            Id = 6,
                            Description = "No description yet :/",
                            ImageName = "neptune.png",
                            Name = "Neptune",
                            RobotsCount = 0,
                            Status = "TODO"
                        },
                        new
                        {
                            Id = 7,
                            Description = "No description yet :/",
                            ImageName = "pluto.png",
                            Name = "Pluto",
                            RobotsCount = 0,
                            Status = "TODO"
                        },
                        new
                        {
                            Id = 8,
                            Description = "No description yet :/",
                            ImageName = "mercury.png",
                            Name = "Mercury",
                            RobotsCount = 0,
                            Status = "TODO"
                        },
                        new
                        {
                            Id = 9,
                            Description = "No description yet :/",
                            ImageName = "venus.png",
                            Name = "Venus",
                            RobotsCount = 0,
                            Status = "TODO"
                        });
                });

            modelBuilder.Entity("Xpand.Api.Models.Robot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Robots");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "R2D2",
                            TeamId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "C3PO",
                            TeamId = 1
                        });
                });

            modelBuilder.Entity("Xpand.Api.Models.Shuttle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId")
                        .IsUnique()
                        .HasFilter("[TeamId] IS NOT NULL");

                    b.ToTable("Shuttles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Lambda T-4a",
                            TeamId = 1
                        });
                });

            modelBuilder.Entity("Xpand.Api.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CaptainId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShuttleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CaptainId = 1,
                            Name = "Team Yoda's",
                            ShuttleId = 1
                        });
                });

            modelBuilder.Entity("Xpand.Api.Models.Captain", b =>
                {
                    b.HasOne("Xpand.Api.Models.Team", "Team")
                        .WithOne("Captain")
                        .HasForeignKey("Xpand.Api.Models.Captain", "TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Xpand.Api.Models.Planet", b =>
                {
                    b.HasOne("Xpand.Api.Models.Team", "Team")
                        .WithMany("Planets")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Xpand.Api.Models.Robot", b =>
                {
                    b.HasOne("Xpand.Api.Models.Team", "Team")
                        .WithMany("Robots")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Xpand.Api.Models.Shuttle", b =>
                {
                    b.HasOne("Xpand.Api.Models.Team", "Team")
                        .WithOne("Shuttle")
                        .HasForeignKey("Xpand.Api.Models.Shuttle", "TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Xpand.Api.Models.Team", b =>
                {
                    b.Navigation("Captain")
                        .IsRequired();

                    b.Navigation("Planets");

                    b.Navigation("Robots");

                    b.Navigation("Shuttle");
                });
#pragma warning restore 612, 618
        }
    }
}
