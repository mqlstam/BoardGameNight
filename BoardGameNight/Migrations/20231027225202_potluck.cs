using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Migrations
{
    /// <inheritdoc />
    public partial class potluck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bordspellenavond_AspNetUsers_OrganisatorId",
                table: "Bordspellenavond");

            migrationBuilder.AlterColumn<string>(
                name: "OrganisatorId",
                table: "Bordspellenavond",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Adres",
                table: "Bordspellenavond",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsPotluck",
                table: "Bordspellenavond",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PotluckItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DietaryRequirements = table.Column<int>(type: "int", nullable: false),
                    BordspellenavondId = table.Column<int>(type: "int", nullable: false),
                    ContributorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotluckItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PotluckItem_AspNetUsers_ContributorId",
                        column: x => x.ContributorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PotluckItem_Bordspellenavond_BordspellenavondId",
                        column: x => x.BordspellenavondId,
                        principalTable: "Bordspellenavond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PotluckItem_BordspellenavondId",
                table: "PotluckItem",
                column: "BordspellenavondId");

            migrationBuilder.CreateIndex(
                name: "IX_PotluckItem_ContributorId",
                table: "PotluckItem",
                column: "ContributorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bordspellenavond_AspNetUsers_OrganisatorId",
                table: "Bordspellenavond",
                column: "OrganisatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bordspellenavond_AspNetUsers_OrganisatorId",
                table: "Bordspellenavond");

            migrationBuilder.DropTable(
                name: "PotluckItem");

            migrationBuilder.DropColumn(
                name: "IsPotluck",
                table: "Bordspellenavond");

            migrationBuilder.AlterColumn<string>(
                name: "OrganisatorId",
                table: "Bordspellenavond",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Adres",
                table: "Bordspellenavond",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_Bordspellenavond_AspNetUsers_OrganisatorId",
                table: "Bordspellenavond",
                column: "OrganisatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
