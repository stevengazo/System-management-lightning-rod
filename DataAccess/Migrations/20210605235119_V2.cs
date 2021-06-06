using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
