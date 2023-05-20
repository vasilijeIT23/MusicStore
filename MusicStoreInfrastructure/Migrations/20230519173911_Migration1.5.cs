using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicStoreInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35);

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { new Guid("30b2a35c-429b-476f-bc6a-ab3f618fdda0"), 1, "Zvucnik" },
                    { new Guid("58822232-3534-446c-8649-0db7c62efdae"), 1, "Harmonika" },
                    { new Guid("5f5f7cff-68d5-466a-a8d6-654ce77c9ac7"), 1, "Pojacalo" },
                    { new Guid("9adc6fc6-4d6d-4695-bb5e-dc68c6bfc04c"), 1, "Gitara" },
                    { new Guid("db5d5cdb-1ccc-4bfc-9c07-5584b121e202"), 1, "Flauta" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[] { new Guid("1e63806d-e978-458f-8a4a-1ea03ed5044f"), 10000, "Warehouse_1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("30b2a35c-429b-476f-bc6a-ab3f618fdda0"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("58822232-3534-446c-8649-0db7c62efdae"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("5f5f7cff-68d5-466a-a8d6-654ce77c9ac7"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9adc6fc6-4d6d-4695-bb5e-dc68c6bfc04c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("db5d5cdb-1ccc-4bfc-9c07-5584b121e202"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("1e63806d-e978-458f-8a4a-1ea03ed5044f"));

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "Customers",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
