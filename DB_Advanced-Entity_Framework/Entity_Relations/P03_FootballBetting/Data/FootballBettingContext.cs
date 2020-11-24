using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Bet> Bets { get; set; } 

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Team>(entity =>
                {
                    entity.HasKey(t => t.TeamId);

                    entity
                    .HasOne(p => p.PrimaryKitColor)
                    .WithMany(pc => pc.PrimaryKitTeams)
                    .HasForeignKey(f => f.PrimaryKitColorId);

                    entity
                   .HasOne(p => p.SecondaryKitColor)
                   .WithMany(pc => pc.SecondaryKitTeams)
                   .HasForeignKey(f => f.SecondaryKitColorId);

                    entity
                    .HasOne(t => t.Town)
                    .WithMany(t => t.Teams)
                    .HasForeignKey(t => t.TownId);
                });

            modelBuilder
                .Entity<Color>(entity =>
                {
                    entity.HasKey(c => c.ColorId);
                });

            modelBuilder
                .Entity<Town>(entity =>
                {
                    entity.HasKey(t => t.TownId);

                    entity
                    .HasOne(c => c.Country)
                    .WithMany(t => t.Towns)
                    .HasForeignKey(e => e.CountryId);
                });

            modelBuilder
                .Entity<Country>(entity =>
                {
                    entity.HasKey(c => c.CountryId);
                });

            modelBuilder
                .Entity<Player>(entity =>
                {
                    entity.HasKey(p => p.PlayerId);

                    entity
                    .HasOne(p => p.Team)
                    .WithMany(t => t.Players)
                    .HasForeignKey(e => e.TeamId);

                    entity
                    .HasOne(p => p.Position)
                    .WithMany(p => p.Players)
                    .HasForeignKey(e => e.PositionId);
                });

            modelBuilder
                .Entity<Position>(entity =>
                {
                    entity.HasKey(p => p.PositionId);
                });

            modelBuilder
                .Entity<PlayerStatistic>(entity =>
                {
                    entity.HasKey(ps => new { ps.PlayerId, ps.GameId });

                    entity
                    .HasOne(ps => ps.Player)
                    .WithMany(p => p.PlayerStatistics)
                    .HasForeignKey(e => e.PlayerId);
                });

            modelBuilder
                .Entity<Game>(entity =>
                {
                    entity.HasKey(g => g.GameId);

                    entity
                    .HasOne(g => g.HomeTeam)
                    .WithMany(ht => ht.HomeGames)
                    .HasForeignKey(g => g.HomeTeamId);

                    entity
                    .HasOne(g => g.AwayTeam)
                    .WithMany(at => at.AwayGames)
                    .HasForeignKey(g => g.AwayTeamId);
                });

            modelBuilder
                .Entity<Bet>(entity =>
                {
                    entity.HasKey(b => b.BetId);
                });

            modelBuilder
                .Entity<User>(entity =>
                {
                    entity.HasKey(u => u.UserId);
                });
        }
    }
}
