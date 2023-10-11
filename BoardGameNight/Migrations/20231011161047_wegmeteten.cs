using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Migrations
{
    /// <inheritdoc />
    public partial class wegmeteten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bordspel_SoortBordspellen_SoortSpelId",
                table: "Bordspel");

            migrationBuilder.DropTable(
                name: "Eten");

            migrationBuilder.AddColumn<int>(
                name: "Dieetwensen",
                table: "Bordspellenavond",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DrankVoorkeur",
                table: "Bordspellenavond",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SoortSpelId",
                table: "Bordspel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bordspel_SoortBordspellen_SoortSpelId",
                table: "Bordspel",
                column: "SoortSpelId",
                principalTable: "SoortBordspellen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bordspel_SoortBordspellen_SoortSpelId",
                table: "Bordspel");

            migrationBuilder.DropColumn(
                name: "Dieetwensen",
                table: "Bordspellenavond");

            migrationBuilder.DropColumn(
                name: "DrankVoorkeur",
                table: "Bordspellenavond");

            migrationBuilder.AlterColumn<int>(
                name: "SoortSpelId",
                table: "Bordspel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Eten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BordspellenavondId = table.Column<int>(type: "int", nullable: false),
                    IsAlcoholvrij = table.Column<bool>(type: "bit", nullable: false),
                    IsLactosevrij = table.Column<bool>(type: "bit", nullable: false),
                    IsNotenvrij = table.Column<bool>(type: "bit", nullable: false),
                    IsVegetarisch = table.Column<bool>(type: "bit", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Eten_BordspellenavondId",
                table: "Eten",
                column: "BordspellenavondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bordspel_SoortBordspellen_SoortSpelId",
                table: "Bordspel",
                column: "SoortSpelId",
                principalTable: "SoortBordspellen",
                principalColumn: "Id");
        }
    }
}
