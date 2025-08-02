using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobOverview.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMillesime : Migration
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

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "GENOMICA", 1f },
                column: "Millesime",
                value: 2023);

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "GENOMICA", 2f },
                column: "Millesime",
                value: 2024);

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "ANATOMIA", 4.5f },
                column: "Millesime",
                value: 2022);

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "ANATOMIA", 5f },
                column: "Millesime",
                value: 2023);

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "ANATOMIA", 5.5f },
                column: "Millesime",
                value: 2024);
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

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "GENOMICA", 1f },
                column: "Millesime",
                value: (short)2023);

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "GENOMICA", 2f },
                column: "Millesime",
                value: (short)2024);

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "ANATOMIA", 4.5f },
                column: "Millesime",
                value: (short)2022);

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "ANATOMIA", 5f },
                column: "Millesime",
                value: (short)2023);

            migrationBuilder.UpdateData(
                table: "Versions",
                keyColumns: new[] { "CodeLogiciel", "Numero" },
                keyValues: new object[] { "ANATOMIA", 5.5f },
                column: "Millesime",
                value: (short)2024);
        }
    }
}
