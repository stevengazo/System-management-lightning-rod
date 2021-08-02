﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(RayosNoDataContext))]
    [Migration("20210728213612_mymigration")]
    partial class mymigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.ClientEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = "7b7a2e75-3e9e-4138-9845-617de0a2c7d4",
                            Name = "Prueba"
                        });
                });

            modelBuilder.Entity("Entities.DeviceEntity", b =>
                {
                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InstallationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecomendedDateOfMaintenance")
                        .HasColumnType("datetime2");

                    b.Property<string>("SaleManId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceId");

                    b.HasIndex("ClientId");

                    b.HasIndex("SaleManId");

                    b.ToTable("Devices");

                    b.HasData(
                        new
                        {
                            DeviceId = "cf3ddd42-1bb4-4d2a-93e4-38ec25163722",
                            Alias = "Prueba",
                            ClientId = "7b7a2e75-3e9e-4138-9845-617de0a2c7d4",
                            InstallationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = false,
                            Latitude = 0f,
                            Longitude = 0f,
                            RecomendedDateOfMaintenance = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SaleManId = "f89a202f-fd0d-46ab-9103-cee7f9fce8ab"
                        });
                });

            modelBuilder.Entity("Entities.IncidentEntity", b =>
                {
                    b.Property<string>("IncidentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContactReportingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DDCEStatus")
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("IncidentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Recomentations")
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportDescripcion")
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<DateTime>("RevisionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevisionInformation")
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<DateTime>("SendReportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TechnicianName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncidentId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Incidents");

                    b.HasData(
                        new
                        {
                            IncidentId = "9615896d-a085-411e-a3d2-c61d60f276f9",
                            DeviceId = "cf3ddd42-1bb4-4d2a-93e4-38ec25163722",
                            IncidentDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReportDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RevisionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SendReportDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Entities.MaintenanceEntity", b =>
                {
                    b.Property<string>("MaintenanceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Ampers")
                        .HasColumnType("real");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("DeviceOhms")
                        .HasColumnType("real");

                    b.Property<DateTime>("MaintenanceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Recomendations")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SpatOhms")
                        .HasColumnType("real");

                    b.Property<string>("StatusOfDevice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TechnicianName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaintenanceId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Maintenances");

                    b.HasData(
                        new
                        {
                            MaintenanceId = "a88601ef-6333-4992-9433-55585f0ec841",
                            Ampers = 0f,
                            DeviceId = "cf3ddd42-1bb4-4d2a-93e4-38ec25163722",
                            DeviceOhms = 0f,
                            MaintenanceDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SpatOhms = 0f
                        });
                });

            modelBuilder.Entity("Entities.ReplacementDeviceEntity", b =>
                {
                    b.Property<string>("ReplacementDeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NewSerieDevice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReplacementDeviceId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Replacements");

                    b.HasData(
                        new
                        {
                            ReplacementDeviceId = "5c75ab94-90e9-4c14-803b-863a320517a6",
                            DeviceId = "cf3ddd42-1bb4-4d2a-93e4-38ec25163722"
                        });
                });

            modelBuilder.Entity("Entities.SaleManEntity", b =>
                {
                    b.Property<string>("SaleManId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuantityOfDevice")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SaleManId");

                    b.ToTable("Salemans");

                    b.HasData(
                        new
                        {
                            SaleManId = "f89a202f-fd0d-46ab-9103-cee7f9fce8ab",
                            Name = "Prueba"
                        });
                });

            modelBuilder.Entity("Entities.WarrantyEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateReceived")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Warranties");

                    b.HasData(
                        new
                        {
                            Id = "e7e99600-5266-4e8a-b88b-5640849d0a18",
                            DateReceived = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateSend = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeviceId = "cf3ddd42-1bb4-4d2a-93e4-38ec25163722"
                        });
                });

            modelBuilder.Entity("Entities.DeviceEntity", b =>
                {
                    b.HasOne("Entities.ClientEntity", "Client")
                        .WithMany("Devices")
                        .HasForeignKey("ClientId");

                    b.HasOne("Entities.SaleManEntity", "SaleMan")
                        .WithMany("Devices")
                        .HasForeignKey("SaleManId");

                    b.Navigation("Client");

                    b.Navigation("SaleMan");
                });

            modelBuilder.Entity("Entities.IncidentEntity", b =>
                {
                    b.HasOne("Entities.DeviceEntity", "Device")
                        .WithMany("Incidents")
                        .HasForeignKey("DeviceId");

                    b.Navigation("Device");
                });

            modelBuilder.Entity("Entities.MaintenanceEntity", b =>
                {
                    b.HasOne("Entities.DeviceEntity", "Device")
                        .WithMany("Maintenances")
                        .HasForeignKey("DeviceId");

                    b.Navigation("Device");
                });

            modelBuilder.Entity("Entities.ReplacementDeviceEntity", b =>
                {
                    b.HasOne("Entities.DeviceEntity", "Device")
                        .WithMany("Replacements")
                        .HasForeignKey("DeviceId");

                    b.Navigation("Device");
                });

            modelBuilder.Entity("Entities.WarrantyEntity", b =>
                {
                    b.HasOne("Entities.DeviceEntity", "Device")
                        .WithMany("Warranties")
                        .HasForeignKey("DeviceId");

                    b.Navigation("Device");
                });

            modelBuilder.Entity("Entities.ClientEntity", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("Entities.DeviceEntity", b =>
                {
                    b.Navigation("Incidents");

                    b.Navigation("Maintenances");

                    b.Navigation("Replacements");

                    b.Navigation("Warranties");
                });

            modelBuilder.Entity("Entities.SaleManEntity", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}