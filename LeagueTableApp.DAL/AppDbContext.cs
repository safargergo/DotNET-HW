using LeagueTableApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//using LeagueTableApp.DAL.Entities;

namespace LeagueTableApp.DAL;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sgergo_locladb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
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
    }*/
}