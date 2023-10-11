using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Migrations
{
    /// <inheritdoc />
    public partial class bordspellgenreandsoort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoortSpel",
                table: "Bordspel",
                newName: "SoortSpelId");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Bordspel",
                newName: "GenreId");

            migrationBuilder.CreateTable(
                name: "BordspelGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BordspelGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoortBordspellen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoortBordspellen", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bordspel_GenreId",
                table: "Bordspel",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Bordspel_SoortSpelId",
                table: "Bordspel",
                column: "SoortSpelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bordspel_BordspelGenres_GenreId",
                table: "Bordspel",
                column: "GenreId",
                principalTable: "BordspelGenres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bordspel_SoortBordspellen_SoortSpelId",
                table: "Bordspel",
                column: "SoortSpelId",
                principalTable: "SoortBordspellen",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bordspel_BordspelGenres_GenreId",
                table: "Bordspel");

            migrationBuilder.DropForeignKey(
                name: "FK_Bordspel_SoortBordspellen_SoortSpelId",
                table: "Bordspel");

            migrationBuilder.DropTable(
                name: "BordspelGenres");

            migrationBuilder.DropTable(
                name: "SoortBordspellen");

            migrationBuilder.DropIndex(
                name: "IX_Bordspel_GenreId",
                table: "Bordspel");

            migrationBuilder.DropIndex(
                name: "IX_Bordspel_SoortSpelId",
                table: "Bordspel");

            migrationBuilder.RenameColumn(
                name: "SoortSpelId",
                table: "Bordspel",
                newName: "SoortSpel");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Bordspel",
                newName: "Genre");
        }
    }
}
