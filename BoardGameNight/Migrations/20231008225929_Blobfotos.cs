using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Migrations
{
    /// <inheritdoc />
    public partial class Blobfotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Bordspel");

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Bordspel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Bordspel");

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Bordspel",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
