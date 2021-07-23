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
                    MaintenanceMonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    ReportDescripcion = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    RevisionInformation = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    DDCEStatus = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    SpatOhms = table.Column<float>(type: "real", nullable: false),
                    DeviceOhms = table.Column<float>(type: "real", nullable: false),
                    Ampers = table.Column<float>(type: "real", nullable: false),
                    Recomentations = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    TechnicianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                values: new object[] { "cca1a3fb-397b-4d2c-a35e-fc0afe562e01", "Prueba" });

            migrationBuilder.InsertData(
                table: "Salemans",
                columns: new[] { "SaleManId", "Name", "QuantityOfDevice" },
                values: new object[] { "464e360a-a0c7-4633-a4ae-dba8e226af38", "Prueba", null });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "Alias", "ClientId", "Country", "InstallationDate", "IsActive", "Latitude", "Longitude", "MaintenanceMonth", "Model", "SaleManId", "Type" },
                values: new object[] { "7ba8af93-7c89-4ccf-876b-6057bd5fa153", "Prueba", "cca1a3fb-397b-4d2c-a35e-fc0afe562e01", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0f, 0f, null, null, "464e360a-a0c7-4633-a4ae-dba8e226af38", null });

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "IncidentId", "Ampers", "DDCEStatus", "DeviceId", "DeviceOhms", "IncidentDate", "Recomentations", "ReportDate", "ReportDescripcion", "RevisionDate", "RevisionInformation", "SpatOhms", "TechnicianName" },
                values: new object[] { "2392104d-f7fc-4f8a-8326-12a317b8b46e", 0f, null, "7ba8af93-7c89-4ccf-876b-6057bd5fa153", 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0f, null });

            migrationBuilder.InsertData(
                table: "Maintenances",
                columns: new[] { "MaintenanceId", "Ampers", "DeviceId", "DeviceOhms", "MaintenanceDate", "ReportId", "SpatOhms", "StatusOfDevice", "TechnicianName" },
                values: new object[] { "2a7eb222-0487-45ea-886b-c3c8fa3c5b02", 0f, "7ba8af93-7c89-4ccf-876b-6057bd5fa153", 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0f, null, null });

            migrationBuilder.InsertData(
                table: "Replacements",
                columns: new[] { "ReplacementDeviceId", "DeviceId", "NewSerieDevice", "Notes" },
                values: new object[] { "b0f73891-e2ce-4085-88ae-dbb5a9778025", "7ba8af93-7c89-4ccf-876b-6057bd5fa153", null, null });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "DateReceived", "DateSend", "DeviceId", "Notes" },
                values: new object[] { "29d0ff20-e7b3-405a-b67b-0180b908e2b6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7ba8af93-7c89-4ccf-876b-6057bd5fa153", null });

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
            ALTER PROCEDURE [dbo].[SearchClientByName]
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
            migrationBuilder.Sql(sp1);
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
