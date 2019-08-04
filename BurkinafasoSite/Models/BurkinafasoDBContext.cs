using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BurkinafasoSite.Models
{
    public partial class BurkinafasoDBContext : DbContext
    {
        public BurkinafasoDBContext()
        {
        }

        public BurkinafasoDBContext(DbContextOptions<BurkinafasoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Facts> Facts { get; set; }
        public virtual DbSet<GeneralDocument> GeneralDocument { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<PracticalExercise> PracticalExercise { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TheoreticalExercise> TheoreticalExercise { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost; Database=BurkinafasoDB ;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Facts>(entity =>
            {
                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.ModuleId).HasColumnName("Module_Id");

                entity.Property(e => e.Topic)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.FactsNavigation)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK__Facts__Module_Id__03F0984C");
            });

            modelBuilder.Entity<GeneralDocument>(entity =>
            {
                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FkModuleId).HasColumnName("Fk_Module_Id");

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkModule)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.FkModuleId)
                    .HasConstraintName("FK__Image__Fk_Module__01142BA1");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.Facts)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.FkCourseId).HasColumnName("Fk_Course_Id");

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Module)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("FK__Module__Fk_Cours__5EBF139D");
            });

            modelBuilder.Entity<PracticalExercise>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(5000);

                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FkModuleId).HasColumnName("Fk_Module_Id");

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.Topic)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkModule)
                    .WithMany(p => p.PracticalExercise)
                    .HasForeignKey(d => d.FkModuleId)
                    .HasConstraintName("FK__Practical__Fk_Mo__7B5B524B");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(5000);

                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FkModuleId).HasColumnName("Fk_Module_Id");

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.Topic)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkModule)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.FkModuleId)
                    .HasConstraintName("FK__Test__Fk_Module___787EE5A0");
            });

            modelBuilder.Entity<TheoreticalExercise>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(5000);

                entity.Property(e => e.Extn)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FkModuleId).HasColumnName("Fk_Module_Id");

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.Topic)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkModule)
                    .WithMany(p => p.TheoreticalExercise)
                    .HasForeignKey(d => d.FkModuleId)
                    .HasConstraintName("FK__Theoretic__Fk_Mo__7E37BEF6");
            });
        }
    }
}
