using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobOverview.Migrations
{
    /// <inheritdoc />
    public partial class CreationBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filieres",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filieres", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Logiciels",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CodeFiliere = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logiciels", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Logiciels_Filieres_CodeFiliere",
                        column: x => x.CodeFiliere,
                        principalTable: "Filieres",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CodeLogiciel = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    CodeModuleParent = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CodeLogicielParent = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => new { x.Code, x.CodeLogiciel });
                    table.ForeignKey(
                        name: "FK_Modules_Logiciels_CodeLogiciel",
                        column: x => x.CodeLogiciel,
                        principalTable: "Logiciels",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Modules_Modules_CodeModuleParent_CodeLogicielParent",
                        columns: x => new { x.CodeModuleParent, x.CodeLogicielParent },
                        principalTable: "Modules",
                        principalColumns: new[] { "Code", "CodeLogiciel" });
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    Numero = table.Column<float>(type: "real", nullable: false),
                    CodeLogiciel = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Millesime = table.Column<short>(type: "smallint", nullable: false),
                    DateOuverture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSortiePrevue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSortieReelle = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => new { x.Numero, x.CodeLogiciel });
                    table.ForeignKey(
                        name: "FK_Versions_Logiciels_CodeLogiciel",
                        column: x => x.CodeLogiciel,
                        principalTable: "Logiciels",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Releases",
                columns: table => new
                {
                    Numero = table.Column<short>(type: "smallint", nullable: false),
                    NumeroVersion = table.Column<float>(type: "real", nullable: false),
                    CodeLogiciel = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DatePubli = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => new { x.Numero, x.NumeroVersion, x.CodeLogiciel });
                    table.ForeignKey(
                        name: "FK_Releases_Versions_NumeroVersion_CodeLogiciel",
                        columns: x => new { x.NumeroVersion, x.CodeLogiciel },
                        principalTable: "Versions",
                        principalColumns: new[] { "Numero", "CodeLogiciel" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Filieres",
                columns: new[] { "Code", "Nom" },
                values: new object[,]
                {
                    { "BIOA", "Support animale" },
                    { "BIOH", "Biologie humaine" },
                    { "BIOV", "Biologie végétale" }
                });

            migrationBuilder.InsertData(
                table: "Logiciels",
                columns: new[] { "Code", "CodeFiliere", "Nom" },
                values: new object[,]
                {
                    { "ANATOMIA", "BIOH", "Anatomia" },
                    { "GENOMICA", "BIOH", "Genomica" }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Code", "CodeLogiciel", "CodeLogicielParent", "CodeModuleParent", "Nom" },
                values: new object[,]
                {
                    { "FONC", "ANATOMIA", null, null, "Anatomie fonctionnelle" },
                    { "MICRO", "ANATOMIA", null, null, "Anatomie microscopique" },
                    { "PARAMETRES", "GENOMICA", null, null, "Paramètres" },
                    { "PATHO", "ANATOMIA", null, null, "Anatomie pathologique" },
                    { "POLYMORPHISME", "GENOMICA", null, null, "Polymorphisme génétique" },
                    { "RADIO", "ANATOMIA", null, null, "Anatomie radiologique" },
                    { "SEQUENCAGE", "GENOMICA", null, null, "Séquençage" },
                    { "TOPO", "ANATOMIA", null, null, "Anatomie topographique" },
                    { "UTILS_ROLES", "GENOMICA", null, null, "Utilisateurs et rôles" },
                    { "VAR_ALLELE", "GENOMICA", null, null, "Variations alléliques" }
                });

            migrationBuilder.InsertData(
                table: "Versions",
                columns: new[] { "CodeLogiciel", "Numero", "DateOuverture", "DateSortiePrevue", "DateSortieReelle", "Millesime" },
                values: new object[,]
                {
                    { "GENOMICA", 1f, new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)2023 },
                    { "GENOMICA", 2f, new DateTime(2022, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)2024 },
                    { "ANATOMIA", 4.5f, new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)2022 },
                    { "ANATOMIA", 5f, new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), (short)2023 },
                    { "ANATOMIA", 5.5f, new DateTime(2023, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, (short)2024 }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Code", "CodeLogiciel", "CodeLogicielParent", "CodeModuleParent", "Nom" },
                values: new object[,]
                {
                    { "ANALYSE", "GENOMICA", "GENOMICA", "SEQUENCAGE", "Analyse" },
                    { "MARQUAGE", "GENOMICA", "GENOMICA", "SEQUENCAGE", "Marquage" },
                    { "SEPARATION", "GENOMICA", "GENOMICA", "SEQUENCAGE", "Séparation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logiciels_CodeFiliere",
                table: "Logiciels",
                column: "CodeFiliere");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CodeLogiciel",
                table: "Modules",
                column: "CodeLogiciel");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CodeModuleParent_CodeLogicielParent",
                table: "Modules",
                columns: new[] { "CodeModuleParent", "CodeLogicielParent" });

            migrationBuilder.CreateIndex(
                name: "IX_Releases_NumeroVersion_CodeLogiciel",
                table: "Releases",
                columns: new[] { "NumeroVersion", "CodeLogiciel" });

            migrationBuilder.CreateIndex(
                name: "IX_Versions_CodeLogiciel",
                table: "Versions",
                column: "CodeLogiciel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Releases");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "Logiciels");

            migrationBuilder.DropTable(
                name: "Filieres");
        }
    }
}
