﻿// <auto-generated />
using System;
using Cwiczenia11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cwiczenia11.Migrations
{
    [DbContext(typeof(CodeFirstContext))]
    [Migration("20200606154227_AddedDoctorPatientPrescriptionMedicamentPrescription_MedicamentTables")]
    partial class AddedDoctorPatientPrescriptionMedicamentPrescription_MedicamentTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cwiczenia11.Models.Doctor", b =>
                {
                    b.Property<int>("IdDoctor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Email")
                        .HasColumnType("datetime2")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdDoctor")
                        .HasName("Doctor_PK");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("Cwiczenia11.Models.Medicament", b =>
                {
                    b.Property<int>("IdMedicament")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdMedicament")
                        .HasName("Medicament_PK");

                    b.ToTable("Medicament");
                });

            modelBuilder.Entity("Cwiczenia11.Models.Patient", b =>
                {
                    b.Property<int>("idPatient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("idPatient")
                        .HasName("Patient_PK");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("Cwiczenia11.Models.Prescription", b =>
                {
                    b.Property<int>("idPrescription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DueDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdDoctor")
                        .HasColumnType("int");

                    b.Property<int>("IdPatient")
                        .HasColumnType("int");

                    b.HasKey("idPrescription")
                        .HasName("Prescription_PK");

                    b.HasIndex("IdDoctor");

                    b.HasIndex("IdPatient");

                    b.ToTable("Prescription");
                });

            modelBuilder.Entity("Cwiczenia11.Models.Prescription_Medicament", b =>
                {
                    b.Property<int>("IdPrescription")
                        .HasColumnType("int");

                    b.Property<int>("IdMedicament")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dose")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription", "IdMedicament")
                        .HasName("Prescription_Medicament_PK");

                    b.HasIndex("IdMedicament");

                    b.ToTable("Prescription_Medicament");
                });

            modelBuilder.Entity("Cwiczenia11.Models.Prescription", b =>
                {
                    b.HasOne("Cwiczenia11.Models.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdDoctor")
                        .HasConstraintName("Prescription_Doctor")
                        .IsRequired();

                    b.HasOne("Cwiczenia11.Models.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("IdPatient")
                        .HasConstraintName("Prescription_Patient")
                        .IsRequired();
                });

            modelBuilder.Entity("Cwiczenia11.Models.Prescription_Medicament", b =>
                {
                    b.HasOne("Cwiczenia11.Models.Medicament", "Medicament")
                        .WithMany("Prescriptions_Medicaments")
                        .HasForeignKey("IdMedicament")
                        .HasConstraintName("Prescription_Medicament_Medicament")
                        .IsRequired();

                    b.HasOne("Cwiczenia11.Models.Prescription", "Prescription")
                        .WithMany("Prescriptions_Medicaments")
                        .HasForeignKey("IdPrescription")
                        .HasConstraintName("Prescription_Medicament_Prescription")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
