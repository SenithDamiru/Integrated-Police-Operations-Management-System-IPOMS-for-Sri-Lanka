﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using testproj.Data;

#nullable disable

namespace testproj.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250327160334_NewMigrationone")]
    partial class NewMigrationone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fine", b =>
                {
                    b.Property<int>("FineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FineID"));

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FineAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("FineReason")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TransactionID")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FineID");

                    b.HasIndex("CitizenID");

                    b.ToTable("Fines");
                });

            modelBuilder.Entity("testproj.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("testproj.Models.Citizen", b =>
                {
                    b.Property<int>("CitizenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CitizenID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NationalID")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CitizenID");

                    b.ToTable("Citizens");
                });

            modelBuilder.Entity("testproj.Models.Complaint", b =>
                {
                    b.Property<int>("ComplaintID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComplaintID"));

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<string>("ComplaintDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComplaintStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComplaintTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ComplaintID");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("testproj.Models.CybercrimeReport", b =>
                {
                    b.Property<int>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportID"));

                    b.Property<int?>("AssignedOfficerID")
                        .HasColumnType("int");

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReported")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EvidenceURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ReportID");

                    b.HasIndex("AssignedOfficerID");

                    b.HasIndex("CitizenID");

                    b.ToTable("CybercrimeReports");
                });

            modelBuilder.Entity("testproj.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackID"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeedbackType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("FeedbackID");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("testproj.Models.PoliceOfficer", b =>
                {
                    b.Property<int>("OfficerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfficerID"));

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<int>("StationID")
                        .HasColumnType("int");

                    b.HasKey("OfficerID");

                    b.HasIndex("RoleID");

                    b.HasIndex("StationID");

                    b.ToTable("PoliceOfficers");
                });

            modelBuilder.Entity("testproj.Models.PoliceReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CitizenID");

                    b.ToTable("PoliceReports");
                });

            modelBuilder.Entity("testproj.Models.PoliceRole", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleID");

                    b.ToTable("PoliceRole");
                });

            modelBuilder.Entity("testproj.Models.PoliceStation", b =>
                {
                    b.Property<int>("StationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StationID"));

                    b.Property<string>("Location")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StationID");

                    b.ToTable("PoliceStation");
                });

            modelBuilder.Entity("testproj.Models.RobberyReport", b =>
                {
                    b.Property<int>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportID"));

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReported")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ResponderID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RobberyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RobberyType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StolenItems")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportID");

                    b.HasIndex("CitizenID");

                    b.HasIndex("ResponderID");

                    b.ToTable("RobberyReports");
                });

            modelBuilder.Entity("testproj.Models.SecurityRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<int?>("AssignedOfficerID")
                        .HasColumnType("int");

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RequestReason")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("SecurityDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RequestID");

                    b.HasIndex("AssignedOfficerID");

                    b.HasIndex("CitizenID");

                    b.ToTable("SecurityRequests");
                });

            modelBuilder.Entity("testproj.Models.TrafficReport", b =>
                {
                    b.Property<int>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportID"));

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReported")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ResponderID")
                        .HasColumnType("int");

                    b.Property<int>("SeverityLevel")
                        .HasColumnType("int");

                    b.Property<string>("TrafficType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ReportID");

                    b.HasIndex("CitizenID");

                    b.HasIndex("ResponderID");

                    b.ToTable("TrafficReports");
                });

            modelBuilder.Entity("testproj.Models.testproj.Models.AccidentReport", b =>
                {
                    b.Property<int>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportID"));

                    b.Property<string>("AccidentType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReported")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ResponderID")
                        .HasColumnType("int");

                    b.HasKey("ReportID");

                    b.HasIndex("CitizenID");

                    b.HasIndex("ResponderID");

                    b.ToTable("AccidentReports");
                });

            modelBuilder.Entity("Fine", b =>
                {
                    b.HasOne("testproj.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("testproj.Models.CybercrimeReport", b =>
                {
                    b.HasOne("testproj.Models.PoliceOfficer", "AssignedOfficer")
                        .WithMany()
                        .HasForeignKey("AssignedOfficerID");

                    b.HasOne("testproj.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedOfficer");

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("testproj.Models.PoliceOfficer", b =>
                {
                    b.HasOne("testproj.Models.PoliceRole", "Role")
                        .WithMany("PoliceOfficers")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("testproj.Models.PoliceStation", "Station")
                        .WithMany("PoliceOfficers")
                        .HasForeignKey("StationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("testproj.Models.PoliceReport", b =>
                {
                    b.HasOne("testproj.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("testproj.Models.RobberyReport", b =>
                {
                    b.HasOne("testproj.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("testproj.Models.PoliceOfficer", "Responder")
                        .WithMany()
                        .HasForeignKey("ResponderID");

                    b.Navigation("Citizen");

                    b.Navigation("Responder");
                });

            modelBuilder.Entity("testproj.Models.SecurityRequest", b =>
                {
                    b.HasOne("testproj.Models.PoliceOfficer", "AssignedOfficer")
                        .WithMany()
                        .HasForeignKey("AssignedOfficerID");

                    b.HasOne("testproj.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedOfficer");

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("testproj.Models.TrafficReport", b =>
                {
                    b.HasOne("testproj.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("testproj.Models.PoliceOfficer", "Responder")
                        .WithMany()
                        .HasForeignKey("ResponderID");

                    b.Navigation("Citizen");

                    b.Navigation("Responder");
                });

            modelBuilder.Entity("testproj.Models.testproj.Models.AccidentReport", b =>
                {
                    b.HasOne("testproj.Models.Citizen", "Citizen")
                        .WithMany()
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("testproj.Models.PoliceOfficer", "Responder")
                        .WithMany()
                        .HasForeignKey("ResponderID");

                    b.Navigation("Citizen");

                    b.Navigation("Responder");
                });

            modelBuilder.Entity("testproj.Models.PoliceRole", b =>
                {
                    b.Navigation("PoliceOfficers");
                });

            modelBuilder.Entity("testproj.Models.PoliceStation", b =>
                {
                    b.Navigation("PoliceOfficers");
                });
#pragma warning restore 612, 618
        }
    }
}
