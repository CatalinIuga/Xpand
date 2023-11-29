using Microsoft.EntityFrameworkCore;
using Xpand.Api.Models;

namespace Xpand.Api.Data;

public class XpandDbContext : DbContext
{
    public XpandDbContext(DbContextOptions<XpandDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // setup realationships and constraints
        modelBuilder
            .Entity<Captain>()
            .HasOne(c => c.Team)
            .WithOne(t => t.Captain)
            .HasForeignKey<Team>(t => t.CaptainId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Team>()
            .HasOne(t => t.Captain)
            .WithOne(c => c.Team)
            .HasForeignKey<Captain>(c => c.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Planet>()
            .HasOne(p => p.Team)
            .WithMany(t => t.Planets)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder
            .Entity<Robot>()
            .HasOne(r => r.Team)
            .WithMany(t => t.Robots)
            .HasForeignKey(r => r.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Team>()
            .HasOne(s => s.Shuttle)
            .WithOne(t => t.Team)
            .HasForeignKey<Team>(t => t.ShuttleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Shuttle>()
            .HasOne(s => s.Team)
            .WithOne(t => t.Shuttle)
            .HasForeignKey<Shuttle>(s => s.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Planet>().Property(p => p.Status).HasConversion<string>();

        // Database seeding

        modelBuilder
            .Entity<Captain>()
            .HasData(
                new Captain
                {
                    Id = 1,
                    Name = "Yoda",
                    Password = "4213",
                    TeamId = 1
                }
            );

        modelBuilder
            .Entity<Team>()
            .HasData(
                new Team
                {
                    Id = 1,
                    CaptainId = 1,
                    Name = "Team Yoda's",
                    ShuttleId = 1
                }
            );

        modelBuilder
            .Entity<Planet>()
            .HasData(
                new Planet
                {
                    Id = 1,
                    Name = "Earth",
                    Status = PlanetStatus.OK,
                    TeamId = 1,
                    Description = "On this planet we live.",
                    ImageName = "earth.png",
                    RobotsCount = 2
                },
                new Planet
                {
                    Id = 2,
                    Name = "Mars",
                    Status = PlanetStatus.OK,
                    Description = "This one planet we can live.",
                    TeamId = 1,
                    ImageName = "mars.png",
                    RobotsCount = 2
                },
                new Planet
                {
                    Id = 3,
                    Name = "Jupiter",
                    Status = PlanetStatus.TODO,
                    ImageName = "jupiter.png",
                },
                new Planet
                {
                    Id = 4,
                    Name = "Saturn",
                    Status = PlanetStatus.NotOK,
                    ImageName = "saturn.png",
                    TeamId = 1,
                    Description = "This one planet we can't live.",
                    RobotsCount = 2
                },
                new Planet
                {
                    Id = 5,
                    Name = "Uranus",
                    Status = PlanetStatus.TODO,
                    ImageName = "uranus.png",
                },
                new Planet
                {
                    Id = 6,
                    Name = "Neptune",
                    Status = PlanetStatus.TODO,
                    ImageName = "neptune.png",
                },
                new Planet
                {
                    Id = 7,
                    Name = "Pluto",
                    Status = PlanetStatus.TODO,
                    ImageName = "pluto.png",
                },
                new Planet
                {
                    Id = 8,
                    Name = "Mercury",
                    Status = PlanetStatus.TODO,
                    ImageName = "mercury.png",
                },
                new Planet
                {
                    Id = 9,
                    Name = "Venus",
                    Status = PlanetStatus.TODO,
                    ImageName = "venus.png",
                }
            );

        modelBuilder
            .Entity<Robot>()
            .HasData(
                new Robot
                {
                    Id = 1,
                    Name = "R2D2",
                    TeamId = 1
                },
                new Robot
                {
                    Id = 2,
                    Name = "C3PO",
                    TeamId = 1
                }
            );

        modelBuilder.Entity<Shuttle>().HasData(new Shuttle { Id = 1, TeamId = 1, });
    }

    public DbSet<Captain> Captains { get; set; }

    public DbSet<Planet> Planets { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<Robot> Robots { get; set; }

    public DbSet<Shuttle> Shuttles { get; set; }
}
