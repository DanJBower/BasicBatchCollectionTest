using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SchoolEfCore.Entities;
using SchoolEfCore.Interfaces;

namespace SchoolEfCore.Context
{
    public class SchoolContext : DbContext, ISchoolContext
    {
        public SchoolContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }

        public DbSet<ClassPupil> ClassPupil { get; set; }

        public DbSet<Pupil> Pupils { get; set; }

        public const string Server = "localhost,3306";
        public const string DatabaseName = "batch_school";
        public const string User = "root";
        public const string Password = "";

        public static DbContextOptionsBuilder DefaultOptions => new DbContextOptionsBuilder().UseMySql(
            $"Server={Server};Database={DatabaseName};User={User};",
            mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new Version(8, 0, 18), ServerType.MySql);
            });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.HasIndex(e => e.Id)
                    .HasName("class_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ClassPupil>(entity =>
            {
                entity.HasKey(e => new { e.PupilId, e.ClassId })
                    .HasName("PRIMARY");

                entity.ToTable("class_pupil");

                entity.HasIndex(e => e.ClassId)
                    .HasName("class_pupil_class_id_fk");

                entity.HasIndex(e => new { e.PupilId, e.ClassId })
                    .HasName("class_pupil_pupil_id_class_id_uindex")
                    .IsUnique();

                entity.Property(e => e.PupilId)
                    .HasColumnName("pupil_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassPupil)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("class_pupil_class_id_fk");

                entity.HasOne(d => d.Pupil)
                    .WithMany(p => p.ClassPupil)
                    .HasForeignKey(d => d.PupilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("class_pupil_pupil_id_fk");
            });

            modelBuilder.Entity<Pupil>(entity =>
            {
                entity.ToTable("pupil");

                entity.HasIndex(e => e.Id)
                    .HasName("pupil_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });
        }
    }
}
