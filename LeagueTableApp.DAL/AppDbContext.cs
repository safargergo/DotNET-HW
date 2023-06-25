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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // alternate keys
        modelBuilder.Entity<League>().HasAlternateKey(l => new { l.Name });
        modelBuilder.Entity<Team>().HasAlternateKey(t => new { t.LeagueId, t.Name });

        // seeding
        modelBuilder.Entity<League>().HasData(
            new League("TestLeague1") { Id = 101, Description = "It's a league for testing, for example test changeing values or deleting." },
            new League("TestLeague2") { Id = 102, Description = "It's a league for testing, for example test changeing values or deleting." }
        );
        modelBuilder.Entity<Team>().HasData(
            new Team("TestTeam1", "Player1, Player2, Player3") { Id = 101, Captain = "Player2", Coach = "Test", LeagueId = 101 },
            new Team("TestTeam2", "A, B, C, D") { Id = 102, Captain = "A", Coach = "Guardiola", LeagueId = 101 },
            new Team("TestTeam3", "X, Y, Z") { Id = 103, Captain = "Y", Coach = "Rosssi", LeagueId = 101, },
            new Team("TestTeamForDelete", "jatekos1, jatekos2, jatekos3") { Id = 104, Captain = "jatekos3", Coach = "BelaBacsi", LeagueId = 102, },
            new Team("TestTeamForUpdate", "Aladár, Béla, Csanád, Dániel") { Id = 105, Captain = "Aladár", Coach = "Csercsaszov", LeagueId = 102, }
        );
        modelBuilder.Entity<Match>().HasData(
            new Match() { Id = 1001, LeagueId = 101, HomeTeamId = 101, ForeignTeamId = 102, IsEnded = true, HomeTeamScore = 2, ForeignTeamScore = 1 },
            new Match() { Id = 1002, LeagueId = 101, HomeTeamId = 102, ForeignTeamId = 101 },
            new Match() { Id = 1003, LeagueId = 101, HomeTeamId = 103, ForeignTeamId = 101, IsEnded = true, HomeTeamScore = 0, ForeignTeamScore = 1 },
            new Match() { Id = 1004, LeagueId = 101, HomeTeamId = 101, ForeignTeamId = 103 },
            new Match() { Id = 1005, LeagueId = 101, HomeTeamId = 102, ForeignTeamId = 103 },
            new Match() { Id = 1006, LeagueId = 101, HomeTeamId = 103, ForeignTeamId = 102, IsEnded = true, HomeTeamScore = 2, ForeignTeamScore = 2 }
        );

        // index
        modelBuilder.Entity<Match>().HasIndex(m => m.IsEnded);//.HasFilter("[IsEnded] IS TRUE");

        // soft delete
        modelBuilder.Entity<League>().HasQueryFilter(l => !l.IsDeleted);
        modelBuilder.Entity<Team>().HasQueryFilter(t => !(t.IsDeleted || t.League.IsDeleted));
        //.HasQueryFilter(t => !t.League.IsDeleted);
        modelBuilder.Entity<Match>().HasQueryFilter(m => !(m.IsDeleted
                                                            || (m.League == null || m.League.IsDeleted)
                                                            || (m.ForeignTeam == null || m.ForeignTeam.IsDeleted)
                                                            || (m.HomeTeam == null || m.HomeTeam.League.IsDeleted)
                                                            || (m.ForeignTeam == null || m.ForeignTeam.League.IsDeleted)
                                                            ));
        /*.HasQueryFilter(m => !(m.League == null || m.League.IsDeleted))
        .HasQueryFilter(m => !(m.ForeignTeam == null || m.ForeignTeam.IsDeleted))
        .HasQueryFilter(m => !(m.HomeTeam == null || m.HomeTeam.League.IsDeleted))
        .HasQueryFilter(m => !(m.ForeignTeam == null || m.ForeignTeam.League.IsDeleted));*/

        // konkurencia kezelés
        modelBuilder.Entity<Match>()
            .Property(m => m.RowVersion)
            .IsRowVersion();


        /*
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
        */
    }

}