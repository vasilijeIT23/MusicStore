using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStoreInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MoneySpent",
                table: "Customers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CartValue",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneySpent",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CartValue",
                table: "Carts");
        }
    }
}
