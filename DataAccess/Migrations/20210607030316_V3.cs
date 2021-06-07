using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: "9e60d688-0ce9-4390-b30a-d8f2fbeac1d0");

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: "bcb694e2-d60c-4b7d-9b99-919ddc77f7cd");

            migrationBuilder.DeleteData(
                table: "Salemans",
                keyColumn: "SaleManId",
                keyValue: "0a593c0f-e10b-4351-8c8b-5d8f8c868793");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name" },
                values: new object[] { "2a4448ee-79ce-4067-aa27-bd44f2a647bb", "Prueba" });

            migrationBuilder.InsertData(
                table: "Salemans",
                columns: new[] { "SaleManId", "Name", "QuantityOfDevice" },
                values: new object[] { "c3212716-d68f-464d-bb92-858791d463e4", "Prueba", null });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "Alias", "ClientId", "Country", "InstallationDate", "IsActive", "Latitude", "Longitude", "MaintenanceMonth", "Model", "SaleManId", "Type" },
                values: new object[] { "38fab921-27d5-4016-8687-ec561decf489", "Prueba", "2a4448ee-79ce-4067-aa27-bd44f2a647bb", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0f, 0f, null, null, "c3212716-d68f-464d-bb92-858791d463e4", null });

            migrationBuilder.InsertData(
                table: "Maintenances",
                columns: new[] { "MaintenanceId", "Ampers", "DeviceId", "DeviceOhms", "MaintenanceDate", "ReportId", "SpatOhms", "StatusOfDevice", "TechnicianName" },
                values: new object[] { "a22b1cd4-fad4-4dbe-917c-23502e6115cc", 0f, "38fab921-27d5-4016-8687-ec561decf489", 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0f, null, null });

            migrationBuilder.InsertData(
                table: "Replacements",
                columns: new[] { "ReplacementDeviceId", "DeviceId", "NewSerieDevice", "Notes" },
                values: new object[] { "e8735636-730d-4c57-8863-37d849115227", "38fab921-27d5-4016-8687-ec561decf489", null, null });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "DateReceived", "DateSend", "DeviceId", "Notes" },
                values: new object[] { "b0929a02-37a3-4751-839b-33531855fe58", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "38fab921-27d5-4016-8687-ec561decf489", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Maintenances",
                keyColumn: "MaintenanceId",
                keyValue: "a22b1cd4-fad4-4dbe-917c-23502e6115cc");

            migrationBuilder.DeleteData(
                table: "Replacements",
                keyColumn: "ReplacementDeviceId",
                keyValue: "e8735636-730d-4c57-8863-37d849115227");

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: "b0929a02-37a3-4751-839b-33531855fe58");

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "DeviceId",
                keyValue: "38fab921-27d5-4016-8687-ec561decf489");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: "2a4448ee-79ce-4067-aa27-bd44f2a647bb");

            migrationBuilder.DeleteData(
                table: "Salemans",
                keyColumn: "SaleManId",
                keyValue: "c3212716-d68f-464d-bb92-858791d463e4");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name" },
                values: new object[] { "9e60d688-0ce9-4390-b30a-d8f2fbeac1d0", "Prueba" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "Alias", "ClientId", "Country", "InstallationDate", "IsActive", "Latitude", "Longitude", "MaintenanceMonth", "Model", "SaleManId", "Type" },
                values: new object[] { "bcb694e2-d60c-4b7d-9b99-919ddc77f7cd", "Prueba", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0f, 0f, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Salemans",
                columns: new[] { "SaleManId", "Name", "QuantityOfDevice" },
                values: new object[] { "0a593c0f-e10b-4351-8c8b-5d8f8c868793", "Prueba", null });
        }
    }
}
