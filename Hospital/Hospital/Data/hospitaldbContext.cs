using System;
using System.Collections.Generic;
using Hospital.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hospital.Data
{
    public partial class hospitaldbContext : IdentityDbContext
    {
        public hospitaldbContext()
        {
        }

        public hospitaldbContext(DbContextOptions<hospitaldbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Chamber> Chambers { get; set; } = null!;
        public virtual DbSet<Corps> Corps { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Disease> Diseases { get; set; } = null!;
        public virtual DbSet<DiseasesHistory> DiseasesHistories { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Medecine> Medecines { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServicesHistory> ServicesHistories { get; set; } = null!;
        public virtual DbSet<Staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=hospitaldb;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chamber>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Availability).HasColumnName("availability");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.CorpsId).HasColumnName("corps_id");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender")
                    .IsFixedLength();

                entity.Property(e => e.Number)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.HasOne(d => d.Corps)
                    .WithMany(p => p.Chambers)
                    .HasForeignKey(d => d.CorpsId)
                    .HasConstraintName("FK__Chambers__corps___4222D4EF");
            });

            modelBuilder.Entity<Corps>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorpsId).HasColumnName("corps_id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Corps)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CorpsId)
                    .HasConstraintName("FK__Departmen__corps__398D8EEE");
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<DiseasesHistory>(entity =>
            {
                entity.ToTable("Diseases_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiseaseDate)
                    .HasColumnType("date")
                    .HasColumnName("disease_date");

                entity.Property(e => e.DiseaseId).HasColumnName("disease_id");

                entity.Property(e => e.Notes)
                    .IsUnicode(false)
                    .HasColumnName("notes");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.DiseasesHistories)
                    .HasForeignKey(d => d.DiseaseId)
                    .HasConstraintName("FK__Diseases___disea__5BE2A6F2");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.DiseasesHistories)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__Diseases___patie__5AEE82B9");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.DiseasesHistories)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__Diseases___recip__5CD6CB2B");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.HasMany(d => d.Departments)
                    .WithMany(p => p.Doctors)
                    .UsingEntity<Dictionary<string, object>>(
                        "DepartmentsDoctor",
                        l => l.HasOne<Department>().WithMany().HasForeignKey("DepartmentId").OnDelete(DeleteBehavior.ClientCascade).HasConstraintName("FK__Departmen__depar__3F466844"),
                        r => r.HasOne<Doctor>().WithMany().HasForeignKey("DoctorId").OnDelete(DeleteBehavior.ClientCascade).HasConstraintName("FK__Departmen__docto__3E52440B"),
                        j =>
                        {
                            j.HasKey("DoctorId", "DepartmentId").HasName("PK__Departme__CFBB07262815985D");

                            j.ToTable("Departments_Doctors");

                            j.IndexerProperty<int>("DoctorId").HasColumnName("doctor_id");

                            j.IndexerProperty<int>("DepartmentId").HasColumnName("department_id");
                        });

                entity.HasMany(d => d.Positions)
                    .WithMany(p => p.Doctors)
                    .UsingEntity<Dictionary<string, object>>(
                        "DoctorsPosition",
                        l => l.HasOne<Position>().WithMany().HasForeignKey("PositionId").OnDelete(DeleteBehavior.ClientCascade).HasConstraintName("FK__Doctors_P__posit__4D94879B"),
                        r => r.HasOne<Doctor>().WithMany().HasForeignKey("DoctorId").OnDelete(DeleteBehavior.ClientCascade).HasConstraintName("FK__Doctors_P__docto__4CA06362"),
                        j =>
                        {
                            j.HasKey("DoctorId", "PositionId").HasName("PK__Doctors___AA033B1EAE05DE2D");

                            j.ToTable("Doctors_Positions");

                            j.IndexerProperty<int>("DoctorId").HasColumnName("doctor_id");

                            j.IndexerProperty<int>("PositionId").HasColumnName("position_id");
                        });
            });

            modelBuilder.Entity<Medecine>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MakersCountry)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("makers_country");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.ChamberId).HasColumnName("chamber_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.InsuranceNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("insurance_number");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("passport_number");

                entity.Property(e => e.PassportSeries)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("passport_series");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sex")
                    .IsFixedLength();

                entity.HasOne(d => d.Chamber)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.ChamberId)
                    .HasConstraintName("FK__Patients__chambe__44FF419A");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("salary");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.MedecineId).HasColumnName("medecine_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.PrescribeDate)
                    .HasColumnType("date")
                    .HasColumnName("prescribe_date");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__Recipes__doctor___5629CD9C");

                entity.HasOne(d => d.Medecine)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.MedecineId)
                    .HasConstraintName("FK__Recipes__medecin__5812160E");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__Recipes__patient__571DF1D5");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorpsId).HasColumnName("corps_id");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Corps)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.CorpsId)
                    .HasConstraintName("FK_Services_Corps");
            });

            modelBuilder.Entity<ServicesHistory>(entity =>
            {
                entity.ToTable("Services_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.ServicesHistories)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__Services___patie__628FA481");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServicesHistories)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Services___servi__6383C8BA");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.PositionId).HasColumnName("position_id");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK__Staff__position___49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
