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
    [Migration("20231126120639_DbUpdate")]
    partial class DbUpdate
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
                            Name = "Luke Skywalker",
                            Password = "No password yet :/"
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
                            Description = "No description yet :/",
                            ImageName = "default.png",
                            Name = "Luke Skywalker",
                            RobotsCount = 0,
                            Status = "TODO",
                            TeamId = 1
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

                    b.HasKey("Id");

                    b.ToTable("Shuttles");
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

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CaptainId = 1,
                            Name = "Lambda"
                        });
                });

            modelBuilder.Entity("Xpand.Api.Models.Captain", b =>
                {
                    b.HasOne("Xpand.Api.Models.Team", "Team")
                        .WithOne("Captain")
                        .HasForeignKey("Xpand.Api.Models.Captain", "TeamId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Xpand.Api.Models.Planet", b =>
                {
                    b.HasOne("Xpand.Api.Models.Team", "Team")
                        .WithMany("Planets")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity("Xpand.Api.Models.Team", b =>
                {
                    b.Navigation("Captain")
                        .IsRequired();

                    b.Navigation("Planets");

                    b.Navigation("Robots");
                });
#pragma warning restore 612, 618
        }
    }
}
