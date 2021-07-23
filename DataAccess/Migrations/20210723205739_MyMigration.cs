using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
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
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecomendedDateOfMaintenance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SaleManId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                        name: "FK_Devices_Salemans_SaleManId",
                        column: x => x.SaleManId,
                        principalTable: "Salemans",
                        principalColumn: "SaleManId",
                        onDelete: ReferentialAction.Restrict);
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
                    ReportDescripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    RevisionInformation = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    DDCEStatus = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Recomentations = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    TechnicianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactReportingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    MaintenanceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusOfDevice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpatOhms = table.Column<float>(type: "real", nullable: false),
                    DeviceOhms = table.Column<float>(type: "real", nullable: false),
                    Ampers = table.Column<float>(type: "real", nullable: false),
                    TechnicianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name" },
                values: new object[] { "1a3cdddd-f242-418a-b07c-88f32ef39b85", "Prueba" });

            migrationBuilder.InsertData(
                table: "Salemans",
                columns: new[] { "SaleManId", "Name", "QuantityOfDevice" },
                values: new object[] { "d4f2c5b0-8145-4ae4-90bb-f276393c3aa5", "Prueba", null });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "Alias", "ClientId", "Country", "InstallationDate", "IsActive", "Latitude", "Longitude", "Model", "RecomendedDateOfMaintenance", "SaleManId", "Type" },
                values: new object[] { "d6d5df13-2d47-4e0e-8fe6-72ab0091abc5", "Prueba", "1a3cdddd-f242-418a-b07c-88f32ef39b85", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0f, 0f, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d4f2c5b0-8145-4ae4-90bb-f276393c3aa5", null });

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "IncidentId", "ContactReportingName", "DDCEStatus", "DeviceId", "IncidentDate", "Recomentations", "ReportDate", "ReportDescripcion", "RevisionDate", "RevisionInformation", "SendReportDate", "TechnicianName" },
                values: new object[] { "2aacd7a5-8c15-438c-aa30-59104e456a64", null, null, "d6d5df13-2d47-4e0e-8fe6-72ab0091abc5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "Maintenances",
                columns: new[] { "MaintenanceId", "Ampers", "DeviceId", "DeviceOhms", "MaintenanceDate", "ReportId", "SpatOhms", "StatusOfDevice", "TechnicianName" },
                values: new object[] { "14b29f24-d383-426a-9468-3c3a23d3c2e0", 0f, "d6d5df13-2d47-4e0e-8fe6-72ab0091abc5", 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0f, null, null });

            migrationBuilder.InsertData(
                table: "Replacements",
                columns: new[] { "ReplacementDeviceId", "DeviceId", "NewSerieDevice", "Notes" },
                values: new object[] { "d1cbe8f2-2f75-44f6-ac59-9ee552ea1c69", "d6d5df13-2d47-4e0e-8fe6-72ab0091abc5", null, null });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "DateReceived", "DateSend", "DeviceId", "Notes" },
                values: new object[] { "510ad2b3-0039-43e2-bc52-78a1c37138d0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d6d5df13-2d47-4e0e-8fe6-72ab0091abc5", null });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ClientId",
                table: "Devices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SaleManId",
                table: "Devices",
                column: "SaleManId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_DeviceId",
                table: "Incidents",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_DeviceId",
                table: "Maintenances",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Replacements_DeviceId",
                table: "Replacements",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_DeviceId",
                table: "Warranties",
                column: "DeviceId");



            #region Stored Procedures
            var SP_M_SelectByDeviceId = @"   CREATE PROCEDURE GetMaintenanceByDeviceId
	                                            @_DeviceId varchar(50) 
                                            AS
                                            BEGIN
                                                SET NOCOUNT ON;
                                                SELECT * 
                                                FROM Maintenances
                                                WHERE Maintenances.DeviceId =@_DeviceId
                                            END
                                    ";

            string SP_D_M_ByYear = @"
                CREATE PROCEDURE GetDeviceByUnreallizedMaintenanceByYear 
	                @_Year varchar(4) = '1'
                AS
                BEGIN
	            SET NOCOUNT ON;
                -- Insert statements for procedure here
	                Select Devices.*
	                from Devices left join (Select * 
							                From Maintenances
							                Where YEAR(MaintenanceDate) = @_Year) as ListMaintenance
	                on Devices.DeviceId = ListMaintenance.DeviceId
	                where ListMaintenance.MaintenanceId is  null
                end
                                        ";
            string sp1 = @"
                CREATE PROCEDURE SearchDeviceByDeviceId
	                @_DeviceSearch varchar(450) = '0'
                AS
                BEGIN
	                SET NOCOUNT ON;
                    -- Insert statements for procedure here
	                Select *
	                FROM Devices
	                WHERE Devices.DeviceId like CONCAT('%',@_DeviceSearch,'%')
                END
                GO
            ";
            string sp2 = @"
            create PROCEDURE SearchClientByName
	            -- Add the parameters for the stored procedure here
	            @_Name varchar(30) ='sample'
            AS
            BEGIN
	            SET NOCOUNT ON;
                -- Insert statements for procedure here
	            SELECT *
	            FROM Clients
	            WHERE Clients.Name LIKE  ('%'+@_Name+'%')
            END
            ";
            ; migrationBuilder.Sql(sp1);
            migrationBuilder.Sql(sp2);
            migrationBuilder.Sql(SP_M_SelectByDeviceId);
            migrationBuilder.Sql(SP_D_M_ByYear);
            #endregion
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
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Salemans");
        }
    }
}
