﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(RayosNoDataContext))]
    partial class RayosNoDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.ClientEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
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

                    b.Property<string>("MaintenanceMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaleManId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceId");

                    b.HasIndex("ClientId");

                    b.HasIndex("SaleManId");

                    b.ToTable("Devices");
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
