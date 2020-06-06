using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cwiczenia11.Models
{
    public class CodeFirstContext : DbContext
    {
        public DbSet<Patient> Patient {get;set;}
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }

        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //KLASA PATIENT
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.idPatient).HasName("Patient_PK");

                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.BirthDate).IsRequired();
            });

            //KLASA Prescription
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.idPrescription).HasName("Prescription_PK");

                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();

                entity.HasOne(d => d.Patient)
                        .WithMany(p => p.Prescriptions)
                        .HasForeignKey(d => d.IdPatient)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Prescription_Patient"); ;
                entity.HasOne(d => d.Doctor)
                        .WithMany(p => p.Prescriptions)
                        .HasForeignKey(d => d.IdDoctor)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Prescription_Doctor");
            });

            //KLASA Doctor
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_PK");

                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();

                entity.Property(e => e.IdDoctor).ValueGeneratedNever();
                entity.HasData(seedDoctors());
            });

            //KLASA Medicament
            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_PK");

                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Prescription_Medicament>(entity =>
            {
                entity.HasKey(e => new { e.IdPrescription, e.IdMedicament }).HasName("Prescription_Medicament_PK");

                entity.Property(e => e.Dose).IsRequired();
                entity.Property(e => e.Details).IsRequired();

                entity.HasOne(d => d.Prescription)
                        .WithMany(p => p.Prescriptions_Medicaments)
                        .HasForeignKey(d => d.IdPrescription)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Prescription_Medicament_Prescription");
                entity.HasOne(d => d.Medicament)
                        .WithMany(p => p.Prescriptions_Medicaments)
                        .HasForeignKey(d => d.IdMedicament)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Prescription_Medicament_Medicament");

            });
        }

        private ICollection<Doctor> seedDoctors()
        {
            ICollection<Doctor> Doctors = new HashSet<Doctor>() {
            new Doctor() {
                IdDoctor = 1,
                FirstName = "Jan",
                LastName = "Nowak",
                Email = "janNowak@wp.pl"
                } ,
            new Doctor() {
                IdDoctor = 2,
                FirstName = "Ola",
                LastName = "Kawka",
                Email = "olakawka@wp.pl"
                } ,
            new Doctor() {
                IdDoctor = 3,
                FirstName = "Ziemowit",
                LastName = "Pędziwiatr",
                Email = "ziemiwiatr@wp.pl"
                } ,
            new Doctor() {
                IdDoctor = 4,
                FirstName = "Pan",
                LastName = "Tadeusz",
                Email = "tadzio2014@wp.pl"
                }
            };
            return Doctors;
        }

    }
}
