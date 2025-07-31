using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobOverview.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifytypeMillesime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Millesime",
                table: "Versions",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Millesime",
                table: "Versions",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
