using System.Diagnostics.Contracts;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsAuctionSystem.Models;

namespace SportsAuctionSystem.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Bids> Bids { get; set; }
        public DbSet<AuctionResults> AuctionResults { get; set; }
        public DbSet<PerformanceReports> PerformanceReports { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Team -> User (ManagerId) relationship
            modelBuilder.Entity<Team>()
                .HasOne(t => t.User)
                .WithMany() // One manager can manage multiple teams
                .HasForeignKey(t => t.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure Player -> User (AgentId) relationship
            modelBuilder.Entity<Players>()
                .HasOne(p => p.User) 
                .WithMany()          // A PlayerAgent can manage multiple players
                .HasForeignKey(p => p.AgentId) // Foreign Key
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Auction>()
            .HasOne(a => a.User)
            .WithMany() // One auctioneer can have multiple auctions
            .HasForeignKey(a => a.AuctioneerId)
            .OnDelete(DeleteBehavior.Restrict);

            // Bids -> Auctions (AuctionId)
            modelBuilder.Entity<Bids>()
                .HasOne(b => b.Auction)
                .WithMany() // An auction can have many bids
                .HasForeignKey(b => b.AuctionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bids -> Players (PlayerId)
            modelBuilder.Entity<Bids>()
                .HasOne(b => b.Player)
                .WithMany() 
                .HasForeignKey(b => b.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bids -> Teams (TeamId)
            modelBuilder.Entity<Bids>()
                .HasOne(b => b.Team)
                .WithMany() 
                .HasForeignKey(b => b.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // AuctionResults -> Auctions (AuctionId)
            modelBuilder.Entity<AuctionResults>()
                .HasOne(ar => ar.Auction)
                .WithMany() 
                .HasForeignKey(ar => ar.AuctionId)
                .OnDelete(DeleteBehavior.Restrict);

            // AuctionResults -> Players (PlayerId)
            modelBuilder.Entity<AuctionResults>()
                .HasOne(ar => ar.Player)
                .WithMany() 
                .HasForeignKey(ar => ar.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // AuctionResults -> Teams (WinningTeamId)
            modelBuilder.Entity<AuctionResults>()
                .HasOne(ar => ar.Team)
                .WithMany() 
                .HasForeignKey(ar => ar.WinningTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // PerformanceReports -> Players (PlayerId)
            modelBuilder.Entity<PerformanceReports>()
                .HasOne(pr => pr.Player)
                .WithMany() 
                .HasForeignKey(pr => pr.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // PerformanceReports -> Users (AnalystId)
            modelBuilder.Entity<PerformanceReports>()
                .HasOne(pr => pr.Analyst)
                .WithMany() 
                .HasForeignKey(pr => pr.AnalystId)
                .OnDelete(DeleteBehavior.Restrict);

            // Finance -> Teams (TeamId)
            modelBuilder.Entity<Finance>()
                .HasOne(f => f.Team)
                .WithMany() 
                .HasForeignKey(f => f.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contracts -> Players (PlayerId)
            modelBuilder.Entity<Contracts>()
                .HasOne(c => c.Player)
                .WithMany() 
                .HasForeignKey(c => c.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contracts -> Teams (TeamId)
            modelBuilder.Entity<Contracts>()
                .HasOne(c => c.Team)
                .WithMany() 
                .HasForeignKey(c => c.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reports -> Users (CreatedBy)
            modelBuilder.Entity<Reports>()
            .HasOne(r => r.CreatedByUser)
            .WithMany() 
            .HasForeignKey(r => r.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

            // Notifications -> Users (UserId)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany() 
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}
