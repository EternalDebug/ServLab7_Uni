using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIuni.Models
{
    public partial class universityContext : DbContext
    {
        public universityContext()
        {
        }

        public universityContext(DbContextOptions<universityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<RankingCriterion> RankingCriteria { get; set; } = null!;
        public virtual DbSet<RankingSystem> RankingSystems { get; set; } = null!;
        public virtual DbSet<University> Universities { get; set; } = null!;
        public virtual DbSet<UniversityRankingYear> UniversityRankingYears { get; set; } = null!;
        public virtual DbSet<UniversityYear> UniversityYears { get; set; } = null!;
        public virtual DbSet<Uzver> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseNpgsql("Host=127.0.0.1;Username=postgres;Password=1949;Port=5432;Database=postgres;");
                optionsBuilder.UseNpgsql("Host=172.17.0.2;Username=postgres;Password=1949;Port=5432;Database=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CountryName).HasColumnName("country_name");
            });

            modelBuilder.Entity<RankingCriterion>(entity =>
            {
                entity.ToTable("ranking_criteria");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CriteriaName).HasColumnName("criteria_name");

                entity.Property(e => e.RankingSystemId).HasColumnName("ranking_system_id");

                entity.HasOne(d => d.RankingSystem)
                    .WithMany(p => p.RankingCriteria)
                    .HasForeignKey(d => d.RankingSystemId);
            });

            modelBuilder.Entity<RankingSystem>(entity =>
            {
                entity.ToTable("ranking_system");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.SystemName).HasColumnName("system_name");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.ToTable("university");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.UniversityName).HasColumnName("university_name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Universities)
                    .HasForeignKey(d => d.CountryId);
            });

            modelBuilder.Entity<UniversityRankingYear>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(e => new { e.UniversityId, e.RankingCriteriaId, e.Year });

                entity.ToTable("university_ranking_year");

                entity.Property(e => e.RankingCriteriaId).HasColumnName("ranking_criteria_id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.UniversityId).HasColumnName("university_id");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.RankingCriteria)
                    .WithMany()
                    .HasForeignKey(d => d.RankingCriteriaId);

                entity.HasOne(d => d.University)
                    .WithMany()
                    .HasForeignKey(d => d.UniversityId);
            });

            modelBuilder.Entity<UniversityYear>(entity =>
            {
                //entity.HasNoKey();

                entity.HasKey(e => new { e.UniversityId, e.Year });

                entity.ToTable("university_year");

                entity.Property(e => e.NumStudents).HasColumnName("num_students");

                entity.Property(e => e.PctFemaleStudents).HasColumnName("pct_female_students");

                entity.Property(e => e.PctInternationalStudents).HasColumnName("pct_international_students");

                entity.Property(e => e.StudentStaffRatio).HasColumnName("student_staff_ratio");

                entity.Property(e => e.UniversityId).HasColumnName("university_id");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.University)
                    .WithMany()
                    .HasForeignKey(d => d.UniversityId);
            });

            modelBuilder.Entity<Uzver>(entity =>
            {
                //entity.HasNoKey();

                entity.HasKey(e => new { e.Username, e.Password });

                entity.ToTable("users");

                entity.Property(e => e.Username).HasColumnName("username");

                entity.Property(e => e.Password).HasColumnName("userpassword");

                entity.Property(e => e.Role).HasColumnName("userrole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
