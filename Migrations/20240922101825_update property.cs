using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dashboardManger.Migrations
{
    /// <inheritdoc />
    public partial class updateproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GUID",
                table: "Property",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GUID",
                table: "Property",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldDefaultValueSql: "NEWID()");
        }
    }
}
