using System;
using System.Collections.Generic;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AlbumItem> AlbumItems { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("Album");

            entity.HasIndex(e => new { e.Year, e.Month, e.Day, e.Name }, "Idx_Album_YearMonthDayName");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.UsrCreatedNavigation).WithMany(p => p.Albums)
                .HasForeignKey(d => d.UsrCreated)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Album_UserAccount");
        });

        modelBuilder.Entity<AlbumItem>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("AlbumItem");

            entity.HasIndex(e => new { e.Album, e.SortOrder }, "Ix_AlbumItem_AlbumSortOrder");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.ContentType)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Hash)
                .HasMaxLength(44)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AlbumNavigation).WithMany(p => p.AlbumItems)
                .HasForeignKey(d => d.Album)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumItem_Album");

            entity.HasOne(d => d.UsrCreatedNavigation).WithMany(p => p.AlbumItems)
                .HasForeignKey(d => d.UsrCreated)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbumItem_UserAccount");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.RevokedAt).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Hash, "Idx_User_Hash");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Hash)
                .HasMaxLength(44)
                .IsUnicode(false);
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(64);
            entity.Property(e => e.PasswordSalt).HasMaxLength(128);
            entity.Property(e => e.ResetToken)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
