using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DBTestingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Installers",
                columns: table => new
                {
                    InstallerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    initDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installers", x => x.InstallerId);
                });

            migrationBuilder.CreateTable(
                name: "ModelDevices",
                columns: table => new
                {
                    ModelDeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelDeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelDevices", x => x.ModelDeviceId);
                });

            migrationBuilder.CreateTable(
                name: "Salemans",
                columns: table => new
                {
                    SaleManId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityOfDevice = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salemans", x => x.SaleManId);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    SectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.SectorId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    TechnicianId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicianName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.TechnicianId);
                });

            migrationBuilder.CreateTable(
                name: "TypesDevices",
                columns: table => new
                {
                    TypeDeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeDeviceName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesDevices", x => x.TypeDeviceId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "SectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecomendedDateOfMaintenance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsReplaced = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelDeviceId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(4)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SaleManId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InstallerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TypeDeviceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Installers_InstallerId",
                        column: x => x.InstallerId,
                        principalTable: "Installers",
                        principalColumn: "InstallerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_ModelDevices_ModelDeviceId",
                        column: x => x.ModelDeviceId,
                        principalTable: "ModelDevices",
                        principalColumn: "ModelDeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_Salemans_SaleManId",
                        column: x => x.SaleManId,
                        principalTable: "Salemans",
                        principalColumn: "SaleManId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_TypesDevices_TypeDeviceId",
                        column: x => x.TypeDeviceId,
                        principalTable: "TypesDevices",
                        principalColumn: "TypeDeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    IncidentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncidentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportDescripcion = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    RevisionInformation = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DDCEStatus = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Recomentations = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ContactReportingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TechnicianId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_Incidents_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "TechnicianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    MaintenanceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusOfDevice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpatOhms = table.Column<float>(type: "real", nullable: false),
                    DeviceOhms = table.Column<float>(type: "real", nullable: false),
                    Ampers = table.Column<float>(type: "real", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recomendations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TechnicianId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_Maintenances_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenances_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "TechnicianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replacements",
                columns: table => new
                {
                    ReplacementDeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NewSerieDevice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replacements", x => x.ReplacementDeviceId);
                    table.ForeignKey(
                        name: "FK_Replacements_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warranties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateSend = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warranties_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warranties_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[] { "506", "Costa Rica" });

            migrationBuilder.InsertData(
                table: "Installers",
                columns: new[] { "InstallerId", "Name", "initDate" },
                values: new object[] { "CR-1", "Grupo Mecsa", new DateTime(2022, 2, 7, 10, 51, 16, 719, DateTimeKind.Local).AddTicks(7157) });

            migrationBuilder.InsertData(
                table: "ModelDevices",
                columns: new[] { "ModelDeviceId", "ModelDeviceName" },
                values: new object[] { 1, "DDCE-100" });

            migrationBuilder.InsertData(
                table: "Salemans",
                columns: new[] { "SaleManId", "Name", "QuantityOfDevice" },
                values: new object[] { "9ff1dd6f-411d-4cbc-8035-f6c188f8d5b4", "sample", null });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "SectorId", "SectorName" },
                values: new object[,]
                {
                    { 1, "Privado" },
                    { 2, "Publico" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Garantia Emitida" },
                    { 2, "Garantia No Emitida" },
                    { 3, "En tramite" },
                    { 4, "Con problemas" }
                });

            migrationBuilder.InsertData(
                table: "Technicians",
                columns: new[] { "TechnicianId", "TechnicianName" },
                values: new object[] { 1, "Sample" });

            migrationBuilder.InsertData(
                table: "TypesDevices",
                columns: new[] { "TypeDeviceId", "TypeDeviceName" },
                values: new object[,]
                {
                    { 1, "Venta" },
                    { 4, "Alquiler" },
                    { 2, "Leasing" },
                    { 3, "Prueba" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name", "SectorId" },
                values: new object[] { "77dd219b-50b0-45b5-8ab5-c3830c68369d", "Prueba", 1 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "Alias", "ClientId", "CountryId", "InstallationDate", "InstallerId", "IsActive", "IsReplaced", "Latitude", "Longitude", "ModelDeviceId", "Note", "RecomendedDateOfMaintenance", "SaleManId", "TypeDeviceId" },
                values: new object[] { "7175e2c3-f855-442a-9613-a846af722a91", "Prueba", "77dd219b-50b0-45b5-8ab5-c3830c68369d", "506", new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Local), null, true, false, null, null, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9ff1dd6f-411d-4cbc-8035-f6c188f8d5b4", 1 });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "Alias", "ClientId", "CountryId", "InstallationDate", "InstallerId", "IsActive", "IsReplaced", "Latitude", "Longitude", "ModelDeviceId", "Note", "RecomendedDateOfMaintenance", "SaleManId", "TypeDeviceId" },
                values: new object[] { "cd93588f-e16c-46e3-943f-c613e9caf0f7", "Reempleazo Prueba", "77dd219b-50b0-45b5-8ab5-c3830c68369d", "506", new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Local), "CR-1", false, true, null, null, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9ff1dd6f-411d-4cbc-8035-f6c188f8d5b4", 1 });

            migrationBuilder.InsertData(
                table: "Replacements",
                columns: new[] { "ReplacementDeviceId", "DeviceId", "NewSerieDevice", "Notes" },
                values: new object[] { "3711b7f2-e590-4bc4-9a7d-309cf2ee2ac7", "cd93588f-e16c-46e3-943f-c613e9caf0f7", "7175e2c3-f855-442a-9613-a846af722a91", null });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "DateReceived", "DateSend", "DeviceId", "Notes", "StatusId" },
                values: new object[] { "92205c2e-188b-4292-8d71-cc980df22240", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7175e2c3-f855-442a-9613-a846af722a91", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SectorId",
                table: "Clients",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ClientId",
                table: "Devices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CountryId",
                table: "Devices",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_InstallerId",
                table: "Devices",
                column: "InstallerId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ModelDeviceId",
                table: "Devices",
                column: "ModelDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SaleManId",
                table: "Devices",
                column: "SaleManId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_TypeDeviceId",
                table: "Devices",
                column: "TypeDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_DeviceId",
                table: "Incidents",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_TechnicianId",
                table: "Incidents",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_DeviceId",
                table: "Maintenances",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_TechnicianId",
                table: "Maintenances",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Replacements_DeviceId",
                table: "Replacements",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_DeviceId",
                table: "Warranties",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_StatusId",
                table: "Warranties",
                column: "StatusId");
            StoredProcedure.ExecuteSP(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "Replacements");

            migrationBuilder.DropTable(
                name: "Warranties");

            migrationBuilder.DropTable(
                name: "Technicians");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Installers");

            migrationBuilder.DropTable(
                name: "ModelDevices");

            migrationBuilder.DropTable(
                name: "Salemans");

            migrationBuilder.DropTable(
                name: "TypesDevices");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
