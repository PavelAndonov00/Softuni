using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> Prescriptions { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>(entity =>
                {
                    entity
                    .HasKey(p => p.PatientId);

                    entity
                    .HasMany(p => p.Visitations)
                    .WithOne(v => v.Patient);

                    entity
                    .HasMany(p => p.Diagnoses)
                    .WithOne(d => d.Patient);

                    entity
                    .HasMany(p => p.Prescriptions)
                    .WithOne(p => p.Patient);

                    entity
                    .Property(p => p.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode();

                    entity
                    .Property(p => p.LastName)
                    .HasMaxLength(50)
                    .IsUnicode();

                    entity
                    .Property(p => p.Address)
                    .HasMaxLength(250)
                    .IsUnicode();

                    entity
                    .Property(p => p.Email)
                    .HasMaxLength(80);
                });

            modelBuilder
                .Entity<Visitation>(entity =>
                {
                    entity
                    .HasKey(v => v.VisitationId);

                    entity
                    .Property(v => v.Comments)
                    .HasMaxLength(250)
                    .IsUnicode();
                });

            modelBuilder
                .Entity<Diagnose>(entity =>
                {
                    entity
                    .HasKey(d => d.DiagnoseId);

                    entity
                    .Property(d => d.Name)
                    .HasMaxLength(50)
                    .IsUnicode();

                    entity
                    .Property(d => d.Comments)
                    .HasMaxLength(250)
                    .IsUnicode();
                });

            modelBuilder
                .Entity<Medicament>(entity =>
                {
                    entity
                    .HasKey(m => m.MedicamentId);

                    entity
                    .Property(m => m.Name)
                    .HasMaxLength(50)
                    .IsUnicode();
                });

            modelBuilder
                .Entity<PatientMedicament>(entity =>
                {
                    entity
                    .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

                    entity
                    .HasOne(pm => pm.Patient)
                    .WithMany(pm => pm.Prescriptions)
                    .HasForeignKey(pm => pm.PatientId);

                    entity
                    .HasOne(pm => pm.Medicament)
                    .WithMany(pm => pm.Prescriptions)
                    .HasForeignKey(pm => pm.MedicamentId);
                });

            modelBuilder
                .Entity<Doctor>(entity =>
                {
                    entity
                    .HasKey(d => d.DoctorId);

                    entity
                    .HasMany(d => d.Visitations)
                    .WithOne(v => v.Doctor);

                    entity
                    .Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsUnicode();

                    entity
                    .Property(p => p.Specialty)
                    .HasMaxLength(100)
                    .IsUnicode();
                });
        }
    }
}
