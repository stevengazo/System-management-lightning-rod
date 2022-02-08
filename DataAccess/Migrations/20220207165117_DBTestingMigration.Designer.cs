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
    [Migration("20220207165117_DBTestingMigration")]
    partial class DBTestingMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.ClientEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectorId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = "77dd219b-50b0-45b5-8ab5-c3830c68369d",
                            Name = "Prueba",
                            SectorId = 1
                        });
                });

            modelBuilder.Entity("Entities.CountryEntity", b =>
                {
                    b.Property<string>("CountryId")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryId = "506",
                            CountryName = "Costa Rica"
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

                    b.Property<string>("CountryId")
                        .HasColumnType("nvarchar(4)");

                    b.Property<DateTime>("InstallationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstallerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReplaced")
                        .HasColumnType("bit");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelDeviceId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecomendedDateOfMaintenance")
                        .HasColumnType("datetime2");

                    b.Property<string>("SaleManId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TypeDeviceId")
                        .HasColumnType("int");

                    b.HasKey("DeviceId");

                    b.HasIndex("ClientId");

                    b.HasIndex("CountryId");

                    b.HasIndex("InstallerId");

                    b.HasIndex("ModelDeviceId");

                    b.HasIndex("SaleManId");

                    b.HasIndex("TypeDeviceId");

                    b.ToTable("Devices");

                    b.HasData(
                        new
                        {
                            DeviceId = "7175e2c3-f855-442a-9613-a846af722a91",
                            Alias = "Prueba",
                            ClientId = "77dd219b-50b0-45b5-8ab5-c3830c68369d",
                            CountryId = "506",
                            InstallationDate = new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            IsActive = true,
                            IsReplaced = false,
                            ModelDeviceId = 1,
                            RecomendedDateOfMaintenance = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SaleManId = "9ff1dd6f-411d-4cbc-8035-f6c188f8d5b4",
                            TypeDeviceId = 1
                        },
                        new
                        {
                            DeviceId = "cd93588f-e16c-46e3-943f-c613e9caf0f7",
                            Alias = "Reempleazo Prueba",
                            ClientId = "77dd219b-50b0-45b5-8ab5-c3830c68369d",
                            CountryId = "506",
                            InstallationDate = new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            InstallerId = "CR-1",
                            IsActive = false,
                            IsReplaced = true,
                            ModelDeviceId = 1,
                            RecomendedDateOfMaintenance = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SaleManId = "9ff1dd6f-411d-4cbc-8035-f6c188f8d5b4",
                            TypeDeviceId = 1
                        });
                });

            modelBuilder.Entity("Entities.IncidentEntity", b =>
                {
                    b.Property<string>("IncidentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContactReportingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DDCEStatus")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("IncidentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Recomentations")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportDescripcion")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RevisionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevisionInformation")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SendReportDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TechnicianId")
                        .HasColumnType("int");

                    b.HasKey("IncidentId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("Entities.InstallerEntity", b =>
                {
                    b.Property<string>("InstallerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("initDate")
                        .HasColumnType("datetime2");

                    b.HasKey("InstallerId");

                    b.ToTable("Installers");

                    b.HasData(
                        new
                        {
                            InstallerId = "CR-1",
                            Name = "Grupo Mecsa",
                            initDate = new DateTime(2022, 2, 7, 10, 51, 16, 719, DateTimeKind.Local).AddTicks(7157)
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SpatOhms")
                        .HasColumnType("real");

                    b.Property<string>("StatusOfDevice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TechnicianId")
                        .HasColumnType("int");

                    b.HasKey("MaintenanceId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("Entities.ModelDeviceEntity", b =>
                {
                    b.Property<int>("ModelDeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ModelDeviceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModelDeviceId");

                    b.ToTable("ModelDevices");

                    b.HasData(
                        new
                        {
                            ModelDeviceId = 1,
                            ModelDeviceName = "DDCE-100"
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
                            ReplacementDeviceId = "3711b7f2-e590-4bc4-9a7d-309cf2ee2ac7",
                            DeviceId = "cd93588f-e16c-46e3-943f-c613e9caf0f7",
                            NewSerieDevice = "7175e2c3-f855-442a-9613-a846af722a91"
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
                            SaleManId = "9ff1dd6f-411d-4cbc-8035-f6c188f8d5b4",
                            Name = "sample"
                        });
                });

            modelBuilder.Entity("Entities.SectorEntity", b =>
                {
                    b.Property<int>("SectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SectorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SectorId");

                    b.ToTable("Sectors");

                    b.HasData(
                        new
                        {
                            SectorId = 1,
                            SectorName = "Privado"
                        },
                        new
                        {
                            SectorId = 2,
                            SectorName = "Publico"
                        });
                });

            modelBuilder.Entity("Entities.StatusEntity", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("StatusId");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            StatusName = "Garantia Emitida"
                        },
                        new
                        {
                            StatusId = 2,
                            StatusName = "Garantia No Emitida"
                        },
                        new
                        {
                            StatusId = 3,
                            StatusName = "En tramite"
                        },
                        new
                        {
                            StatusId = 4,
                            StatusName = "Con problemas"
                        });
                });

            modelBuilder.Entity("Entities.TechnicianEntity", b =>
                {
                    b.Property<int>("TechnicianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TechnicianName")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("TechnicianId");

                    b.ToTable("Technicians");

                    b.HasData(
                        new
                        {
                            TechnicianId = 1,
                            TechnicianName = "Sample"
                        });
                });

            modelBuilder.Entity("Entities.TypeDeviceEntity", b =>
                {
                    b.Property<int>("TypeDeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeDeviceName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("TypeDeviceId");

                    b.ToTable("TypesDevices");

                    b.HasData(
                        new
                        {
                            TypeDeviceId = 1,
                            TypeDeviceName = "Venta"
                        },
                        new
                        {
                            TypeDeviceId = 4,
                            TypeDeviceName = "Alquiler"
                        },
                        new
                        {
                            TypeDeviceId = 2,
                            TypeDeviceName = "Leasing"
                        },
                        new
                        {
                            TypeDeviceId = 3,
                            TypeDeviceName = "Prueba"
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

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("StatusId");

                    b.ToTable("Warranties");

                    b.HasData(
                        new
                        {
                            Id = "92205c2e-188b-4292-8d71-cc980df22240",
                            DateReceived = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateSend = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeviceId = "7175e2c3-f855-442a-9613-a846af722a91",
                            StatusId = 1
                        });
                });

            modelBuilder.Entity("Entities.ClientEntity", b =>
                {
                    b.HasOne("Entities.SectorEntity", "Sector")
                        .WithMany("Clients")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sector");
                });

            modelBuilder.Entity("Entities.DeviceEntity", b =>
                {
                    b.HasOne("Entities.ClientEntity", "Client")
                        .WithMany("Devices")
                        .HasForeignKey("ClientId");

                    b.HasOne("Entities.CountryEntity", "Country")
                        .WithMany("Devices")
                        .HasForeignKey("CountryId");

                    b.HasOne("Entities.InstallerEntity", "installer")
                        .WithMany("devices")
                        .HasForeignKey("InstallerId");

                    b.HasOne("Entities.ModelDeviceEntity", "ModelDevice")
                        .WithMany("Devices")
                        .HasForeignKey("ModelDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.SaleManEntity", "SaleMan")
                        .WithMany("Devices")
                        .HasForeignKey("SaleManId");

                    b.HasOne("Entities.TypeDeviceEntity", "TypeDevice")
                        .WithMany("Devices")
                        .HasForeignKey("TypeDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Country");

                    b.Navigation("installer");

                    b.Navigation("ModelDevice");

                    b.Navigation("SaleMan");

                    b.Navigation("TypeDevice");
                });

            modelBuilder.Entity("Entities.IncidentEntity", b =>
                {
                    b.HasOne("Entities.DeviceEntity", "Device")
                        .WithMany("Incidents")
                        .HasForeignKey("DeviceId");

                    b.HasOne("Entities.TechnicianEntity", "Technician")
                        .WithMany("Incidents")
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("Entities.MaintenanceEntity", b =>
                {
                    b.HasOne("Entities.DeviceEntity", "Device")
                        .WithMany("Maintenances")
                        .HasForeignKey("DeviceId");

                    b.HasOne("Entities.TechnicianEntity", "Technician")
                        .WithMany("Maintenances")
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Technician");
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

                    b.HasOne("Entities.StatusEntity", "Status")
                        .WithMany("Warranties")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Entities.ClientEntity", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("Entities.CountryEntity", b =>
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

            modelBuilder.Entity("Entities.InstallerEntity", b =>
                {
                    b.Navigation("devices");
                });

            modelBuilder.Entity("Entities.ModelDeviceEntity", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("Entities.SaleManEntity", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("Entities.SectorEntity", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Entities.StatusEntity", b =>
                {
                    b.Navigation("Warranties");
                });

            modelBuilder.Entity("Entities.TechnicianEntity", b =>
                {
                    b.Navigation("Incidents");

                    b.Navigation("Maintenances");
                });

            modelBuilder.Entity("Entities.TypeDeviceEntity", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}