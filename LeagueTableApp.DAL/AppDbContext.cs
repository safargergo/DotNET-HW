using LeagueTableApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;

//using LeagueTableApp.DAL.Entities;

namespace LeagueTableApp.DAL;

public class AppDbContext : DbContext
{
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sgergo_locladb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }*/
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }
    // Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=sgergo_locladb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
    public DbSet<League> Leagues => Set<League>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Team> Teams => Set<Team>();

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>()
            .Property(t => t.Name)
            .HasMaxLength(15);

        modelBuilder.Entity<Team>().HasData(
            new Team("asd") { Id = 1 }
        );

        List<string> asd2players = new List<string>();
        modelBuilder.Entity<Team>().HasData(
            new Team("asd2") { Id = 1, Players = asd2players.to  }//,
            //new Product("Bor") { Id = 2, UnitPrice = 550, CategoryId = 1 }
        );
    }*/

}