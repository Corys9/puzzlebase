using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PuzzleBase.DAL.DB
{
    public partial class PuzzlebaseContext : DbContext
    {
        public string ConnectionString { get; set; }
        public static string DefaultConnectionString { get; set; }

        public PuzzlebaseContext()
        {
        }

        public PuzzlebaseContext(DbContextOptions<PuzzlebaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Puzzle> Puzzle { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql(ConnectionString ?? DefaultConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.WebSite)
                    .HasColumnType("varchar(255)")
                    .HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("history");

                entity.HasIndex(e => e.PuzzleId)
                    .HasName("fk_History_Puzzle_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_History_User_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Duration).HasColumnType("time");

                entity.Property(e => e.LastUpdatedTs)
                    .HasColumnName("LastUpdatedTS")
                    .HasColumnType("datetime");

                entity.Property(e => e.PuzzleId)
                    .HasColumnName("PuzzleID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.State)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Puzzle)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.PuzzleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_History_Puzzle");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_History_User");
            });

            modelBuilder.Entity<Puzzle>(entity =>
            {
                entity.ToTable("puzzle");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("fk_Puzzle_Author_idx");

                entity.HasIndex(e => e.OwnerId)
                    .HasName("fk_Puzzle_User_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("AuthorID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("varchar(2000)")
                    .HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.CreatedTs)
                    .HasColumnName("CreatedTS")
                    .HasColumnType("datetime");

                entity.Property(e => e.Difficulty).HasColumnType("int(11)");

                entity.Property(e => e.OwnerId)
                    .HasColumnName("OwnerID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Puzzle)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_Puzzle_Author");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Puzzle)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_Puzzle_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActivatedTs)
                    .HasColumnName("ActivatedTS")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedTs)
                    .HasColumnName("CreatedTS")
                    .HasColumnType("datetime");

                entity.Property(e => e.DisplayName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.LastLoginTs)
                    .HasColumnName("LastLoginTS")
                    .HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.SuspensionReason)
                    .HasColumnType("varchar(255)")
                    .HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
