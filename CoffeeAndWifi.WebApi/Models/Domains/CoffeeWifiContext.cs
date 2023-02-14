using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoffeeAndWifi.WebApi.Models.Domains;

public partial class CoffeeWifiContext : DbContext
{
    public CoffeeWifiContext()
    {
    }

    public CoffeeWifiContext(DbContextOptions<CoffeeWifiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cafe> Cafes { get; set; }

    public virtual DbSet<CoffeeRating> CoffeeRatings { get; set; }

    public virtual DbSet<PwrSocketsRating> PwrSocketsRatings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WifiRating> WifiRatings { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Coffee&Wifi;User Id=dev_user;Password=123;encrypt=false;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cafe>(entity =>
        {
            entity.Property(e => e.CafeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClosingTime)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LocationUrl)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.OpeningTime)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CoffeeRating).WithMany(p => p.Cafes)
                .HasForeignKey(d => d.CoffeeRatingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cafes_CoffeeRatings");

            entity.HasOne(d => d.PowerSocketsRating).WithMany(p => p.Cafes)
                .HasForeignKey(d => d.PowerSocketsRatingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cafes_PwrSocketsRatings");

            entity.HasOne(d => d.WifiStrengthRating).WithMany(p => p.Cafes)
                .HasForeignKey(d => d.WifiStrengthRatingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cafes_WifiRatings");
        });

        modelBuilder.Entity<CoffeeRating>(entity =>
        {
            entity.Property(e => e.Rating).HasMaxLength(50);
        });

        modelBuilder.Entity<PwrSocketsRating>(entity =>
        {
            entity.Property(e => e.Rating).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<WifiRating>(entity =>
        {
            entity.Property(e => e.Rating).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
