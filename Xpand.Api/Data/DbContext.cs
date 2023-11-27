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
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Planet>().Property(p => p.Status).HasConversion<string>();

        modelBuilder
            .Entity<Robot>()
            .HasOne(r => r.Team)
            .WithMany(t => t.Robots)
            .HasForeignKey(r => r.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder
        //     .Entity<Shuttle>()
        //     .HasOne(s => s.Team)
        //     .WithOne(t => t.Shuttle)
        //     .HasForeignKey<Team>(t => t.ShuttleId)
        //     .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Captain>().HasData(new Captain { Id = 1, });

        modelBuilder
            .Entity<Team>()
            .HasData(
                new Team
                {
                    Id = 1,
                    CaptainId = 1,
                    Name = "Lambda"
                }
            );

        modelBuilder.Entity<Planet>().HasData(new Planet { Id = 1, TeamId = 1, });

        modelBuilder.Entity<Robot>().HasData(new Robot { Id = 1, TeamId = 1, });

        // modelBuilder.Entity<Shuttle>().HasData(new Shuttle { Id = 1, TeamId = 1, });
    }

    public DbSet<Captain> Captains { get; set; }

    public DbSet<Planet> Planets { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<Robot> Robots { get; set; }

    public DbSet<Shuttle> Shuttles { get; set; }
}