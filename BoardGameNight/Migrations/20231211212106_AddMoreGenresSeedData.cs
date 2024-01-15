using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BoardGameNight.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreGenresSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PotluckItem_AspNetUsers_ParticipantId",
                table: "PotluckItem");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantId",
                table: "PotluckItem",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Dieetwensen",
                table: "PotluckItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "BordspelGenres",
                columns: new[] { "Id", "Naam" },
                values: new object[,]
                {
                    { 1, "Strategie" },
                    { 2, "Familie" },
                    { 3, "Avontuur" },
                    { 4, "Kaartspel" },
                    { 5, "Dobbelspel" },
                    { 6, "Educatief" },
                    { 7, "Fantasie" },
                    { 8, "Party" },
                    { 9, "Puzzel" },
                    { 10, "Sport" }
                });

            migrationBuilder.InsertData(
                table: "SoortBordspellen",
                columns: new[] { "Id", "Naam" },
                values: new object[,]
                {
                    { 1, "Abstract Spel" },
                    { 2, "Thematisch Spel" },
                    { 3, "Strategiespel" },
                    { 4, "Familiespel" },
                    { 5, "Kinderspel" },
                    { 6, "Partyspel" },
                    { 7, "Kaartspel" },
                    { 8, "Dobbelspel" },
                    { 9, "Coöperatief Spel" },
                    { 10, "Solo Spel" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PotluckItem_AspNetUsers_ParticipantId",
                table: "PotluckItem",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PotluckItem_AspNetUsers_ParticipantId",
                table: "PotluckItem");

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BordspelGenres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SoortBordspellen",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantId",
                table: "PotluckItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Dieetwensen",
                table: "PotluckItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PotluckItem_AspNetUsers_ParticipantId",
                table: "PotluckItem",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
