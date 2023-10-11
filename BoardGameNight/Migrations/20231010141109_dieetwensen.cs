using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Migrations
{
    /// <inheritdoc />
    public partial class dieetwensen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergieën",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Dieetwensen",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DrankVoorkeur",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrankVoorkeur",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Dieetwensen",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Allergieën",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
