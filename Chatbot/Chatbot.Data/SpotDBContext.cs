using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Chatbot.Data
{
    public partial class SpotDBContext : DbContext
    {
        public virtual DbSet<League> League { get; set; }
        public virtual DbSet<Nflteams> Nflteams { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserInterest> UserInterest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"server=tulsqlweek.database.windows.net;database=SpotDB;user=sqladmin;password=Password1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<League>(entity =>
            {
                entity.ToTable("League", "Spot");

                entity.Property(e => e.LeagueName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Nflteams>(entity =>
            {
                entity.HasKey(e => e.Nflid);

                entity.ToTable("NFLTeams", "Spot");

                entity.Property(e => e.Nflid).HasColumnName("NFLId");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Conference)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserInfo", "Spot");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserInterest>(entity =>
            {
                entity.HasKey(e => e.InterestId);

                entity.ToTable("UserInterest", "Spot");

                entity.Property(e => e.FavAthlete).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Nflid).HasColumnName("NFLId");

                entity.HasOne(d => d.League)
                    .WithMany(p => p.UserInterest)
                    .HasForeignKey(d => d.LeagueId)
                    .HasConstraintName("FK_UserInterest_LeagueId");

                entity.HasOne(d => d.Nfl)
                    .WithMany(p => p.UserInterest)
                    .HasForeignKey(d => d.Nflid)
                    .HasConstraintName("FK_UserInterest_NFLId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInterest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInterest_UserId");
            });
        }
    }
}
