using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicStoreInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("0db56b78-d3e7-4d9f-aa7d-823bc58ead36"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("7beb94ba-fc46-4c38-b93c-9b90fb512f0e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("462e0b5e-ee01-49c0-aee6-466de610ce36"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("637b397c-6067-4d92-8349-b8bec5a5e502"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("771aee63-9224-4454-a09f-58f1c8a0dbf3"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d53b0f4a-43c8-47f7-8c98-b9dadfc80ffb"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e5a60428-440d-4937-82d3-d3060ed473d2"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("e0080d55-9790-430e-9248-b716e4a60bdb"));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Customers",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { new Guid("51336115-4669-42ac-a1bd-ca7352b83b76"), 1, "Zvucnik" },
                    { new Guid("650af2d4-7c7a-44b3-b875-931cc3b0e919"), 1, "Harmonika" },
                    { new Guid("a444d554-17cf-42f1-8375-65895a4d791f"), 1, "Pojacalo" },
                    { new Guid("f9bf7848-214b-477e-9d7e-5e767344f54f"), 1, "Gitara" },
                    { new Guid("fca1b053-dd27-4633-bf19-bd6e6f54f037"), 1, "Flauta" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[] { new Guid("f065aab1-c6e9-4ab1-8d06-aba1371dd049"), 10000, "Warehouse_1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("51336115-4669-42ac-a1bd-ca7352b83b76"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("650af2d4-7c7a-44b3-b875-931cc3b0e919"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a444d554-17cf-42f1-8375-65895a4d791f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("f9bf7848-214b-477e-9d7e-5e767344f54f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fca1b053-dd27-4633-bf19-bd6e6f54f037"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("f065aab1-c6e9-4ab1-8d06-aba1371dd049"));

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MoneySpent", "Role", "Status", "StatusExpirationDate" },
                values: new object[,]
                {
                    { new Guid("0db56b78-d3e7-4d9f-aa7d-823bc58ead36"), "marko@ds.com", "Marko", "Markovic", 0.0, 1, 1, new DateTime(2023, 2, 3, 19, 27, 54, 242, DateTimeKind.Local).AddTicks(2013) },
                    { new Guid("7beb94ba-fc46-4c38-b93c-9b90fb512f0e"), "vasilije.mucibabic@gmail.com", "Vasilije", "Mucibabic", 0.0, 2, 1, new DateTime(2023, 2, 3, 19, 27, 54, 242, DateTimeKind.Local).AddTicks(2102) }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { new Guid("462e0b5e-ee01-49c0-aee6-466de610ce36"), 1, "Pojacalo" },
                    { new Guid("637b397c-6067-4d92-8349-b8bec5a5e502"), 1, "Harmonika" },
                    { new Guid("771aee63-9224-4454-a09f-58f1c8a0dbf3"), 1, "Zvucnik" },
                    { new Guid("d53b0f4a-43c8-47f7-8c98-b9dadfc80ffb"), 1, "Gitara" },
                    { new Guid("e5a60428-440d-4937-82d3-d3060ed473d2"), 1, "Flauta" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[] { new Guid("e0080d55-9790-430e-9248-b716e4a60bdb"), 10000, "Warehouse_1" });
        }
    }
}
