using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicStoreInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("0e34649d-a8cd-47c4-8400-6259d183f05c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("14350249-069c-455a-8ac5-2527edfdaa03"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("2b735c00-19b8-4921-851e-0de5cb693d9c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6ce08fe2-2eed-4b53-a8e0-47e14148fa6b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ba509490-218d-4e30-aafa-883ed4253347"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("fd569164-65d1-429b-8936-b53ebbd4edd3"));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { new Guid("295e4242-a1d7-42a5-9936-3fe65207d081"), 1, "Zvucnik" },
                    { new Guid("5f1ff512-a2c7-4f3f-93c0-e173fac3b067"), 1, "Pojacalo" },
                    { new Guid("62fde825-88b7-47be-98a2-a05fd00eb0f0"), 1, "Gitara" },
                    { new Guid("a5c10ce5-9d31-42bb-b0cc-b49da615ca25"), 1, "Harmonika" },
                    { new Guid("f5c5a94a-a85d-471f-9aa5-50681b1b0f9e"), 1, "Flauta" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[] { new Guid("7923c506-1780-4b4b-a14a-454a058ce745"), 10000, "Warehouse_1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("295e4242-a1d7-42a5-9936-3fe65207d081"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("5f1ff512-a2c7-4f3f-93c0-e173fac3b067"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("62fde825-88b7-47be-98a2-a05fd00eb0f0"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a5c10ce5-9d31-42bb-b0cc-b49da615ca25"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("f5c5a94a-a85d-471f-9aa5-50681b1b0f9e"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("7923c506-1780-4b4b-a14a-454a058ce745"));

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { new Guid("0e34649d-a8cd-47c4-8400-6259d183f05c"), 1, "Harmonika" },
                    { new Guid("14350249-069c-455a-8ac5-2527edfdaa03"), 1, "Zvucnik" },
                    { new Guid("2b735c00-19b8-4921-851e-0de5cb693d9c"), 1, "Gitara" },
                    { new Guid("6ce08fe2-2eed-4b53-a8e0-47e14148fa6b"), 1, "Pojacalo" },
                    { new Guid("ba509490-218d-4e30-aafa-883ed4253347"), 1, "Flauta" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[] { new Guid("fd569164-65d1-429b-8936-b53ebbd4edd3"), 10000, "Warehouse_1" });
        }
    }
}
