using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bordspel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SoortSpel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bordspel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persoon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Geslacht = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Geboortedatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dieetwensen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergieën = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persoon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bordspellenavond",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxAantalSpelers = table.Column<int>(type: "int", nullable: false),
                    DatumTijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    OrganisatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bordspellenavond", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bordspellenavond_Persoon_OrganisatorId",
                        column: x => x.OrganisatorId,
                        principalTable: "Persoon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BordspellenavondBordspel",
                columns: table => new
                {
                    BordspelId = table.Column<int>(type: "int", nullable: false),
                    BordspellenavondId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BordspellenavondBordspel", x => new { x.BordspelId, x.BordspellenavondId });
                    table.ForeignKey(
                        name: "FK_BordspellenavondBordspel_Bordspel_BordspelId",
                        column: x => x.BordspelId,
                        principalTable: "Bordspel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BordspellenavondBordspel_Bordspellenavond_BordspellenavondId",
                        column: x => x.BordspellenavondId,
                        principalTable: "Bordspellenavond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BordspellenavondPersoon",
                columns: table => new
                {
                    BordspellenavondId = table.Column<int>(type: "int", nullable: false),
                    PersoonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BordspellenavondPersoon", x => new { x.BordspellenavondId, x.PersoonId });
                    table.ForeignKey(
                        name: "FK_BordspellenavondPersoon_Bordspellenavond_BordspellenavondId",
                        column: x => x.BordspellenavondId,
                        principalTable: "Bordspellenavond",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BordspellenavondPersoon_Persoon_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "Persoon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Eten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVegetarisch = table.Column<bool>(type: "bit", nullable: false),
                    IsLactosevrij = table.Column<bool>(type: "bit", nullable: false),
                    IsNotenvrij = table.Column<bool>(type: "bit", nullable: false),
                    IsAlcoholvrij = table.Column<bool>(type: "bit", nullable: false),
                    BordspellenavondId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eten_Bordspellenavond_BordspellenavondId",
                        column: x => x.BordspellenavondId,
                        principalTable: "Bordspellenavond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersoonId = table.Column<int>(type: "int", nullable: false),
                    BordspellenavondId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Bordspellenavond_BordspellenavondId",
                        column: x => x.BordspellenavondId,
                        principalTable: "Bordspellenavond",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_Persoon_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "Persoon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bordspellenavond_OrganisatorId",
                table: "Bordspellenavond",
                column: "OrganisatorId");

            migrationBuilder.CreateIndex(
                name: "IX_BordspellenavondBordspel_BordspellenavondId",
                table: "BordspellenavondBordspel",
                column: "BordspellenavondId");

            migrationBuilder.CreateIndex(
                name: "IX_BordspellenavondPersoon_PersoonId",
                table: "BordspellenavondPersoon",
                column: "PersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Eten_BordspellenavondId",
                table: "Eten",
                column: "BordspellenavondId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BordspellenavondId",
                table: "Review",
                column: "BordspellenavondId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_PersoonId",
                table: "Review",
                column: "PersoonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BordspellenavondBordspel");

            migrationBuilder.DropTable(
                name: "BordspellenavondPersoon");

            migrationBuilder.DropTable(
                name: "Eten");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Bordspel");

            migrationBuilder.DropTable(
                name: "Bordspellenavond");

            migrationBuilder.DropTable(
                name: "Persoon");
        }
    }
}
