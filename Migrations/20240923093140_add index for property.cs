using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dashboardManger.Migrations
{
    /// <inheritdoc />
    public partial class addindexforproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Property_GUID",
                table: "Property",
                column: "GUID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Property_GUID",
                table: "Property");
        }
    }
}
