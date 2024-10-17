using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dashboardManger.Migrations
{
    /// <inheritdoc />
    public partial class updatepropertytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorArea",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ListingPrice",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "TotalArea",
                table: "Property");

            migrationBuilder.AddColumn<decimal>(
                name: "Commission",
                table: "Property",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FirstPartPercentage",
                table: "Property",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FirstPartPrice",
                table: "Property",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RestPartPercentage",
                table: "Property",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RestPartPrice",
                table: "Property",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "FirstPartPercentage",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "FirstPartPrice",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "RestPartPercentage",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "RestPartPrice",
                table: "Property");

            migrationBuilder.AddColumn<double>(
                name: "FloorArea",
                table: "Property",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListingPrice",
                table: "Property",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalArea",
                table: "Property",
                type: "float",
                nullable: true);
        }
    }
}
