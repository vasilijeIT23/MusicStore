using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicStoreInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("226afa6f-c8c2-4829-961e-509c1e9cd518"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("22e3bbba-903d-4f44-a9f9-dd96d5cf58ea"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b7323db2-60c0-4b4a-8077-3fdc0a1ca8e2"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b7708bd9-3e51-4db9-95a9-a9027b1c1926"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e72cb1a7-dbaa-420c-b76c-fdb3d26c8402"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("493cf5d1-ba88-4bd9-9332-fd9e22577e8e"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { new Guid("226afa6f-c8c2-4829-961e-509c1e9cd518"), 1, "Harmonika" },
                    { new Guid("22e3bbba-903d-4f44-a9f9-dd96d5cf58ea"), 1, "Gitara" },
                    { new Guid("b7323db2-60c0-4b4a-8077-3fdc0a1ca8e2"), 1, "Pojacalo" },
                    { new Guid("b7708bd9-3e51-4db9-95a9-a9027b1c1926"), 1, "Flauta" },
                    { new Guid("e72cb1a7-dbaa-420c-b76c-fdb3d26c8402"), 1, "Zvucnik" }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[] { new Guid("493cf5d1-ba88-4bd9-9332-fd9e22577e8e"), 10000, "Warehouse_1" });
        }
    }
}
