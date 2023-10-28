using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardGameNight.Migrations
{
    /// <inheritdoc />
    public partial class potluckcreatee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PotluckItem_AspNetUsers_ContributorId",
                table: "PotluckItem");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PotluckItem");

            migrationBuilder.RenameColumn(
                name: "DietaryRequirements",
                table: "PotluckItem",
                newName: "Dieetwensen");

            migrationBuilder.RenameColumn(
                name: "ContributorId",
                table: "PotluckItem",
                newName: "ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_PotluckItem_ContributorId",
                table: "PotluckItem",
                newName: "IX_PotluckItem_ParticipantId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PotluckItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PotluckItem_AspNetUsers_ParticipantId",
                table: "PotluckItem",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PotluckItem_AspNetUsers_ParticipantId",
                table: "PotluckItem");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PotluckItem");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "PotluckItem",
                newName: "ContributorId");

            migrationBuilder.RenameColumn(
                name: "Dieetwensen",
                table: "PotluckItem",
                newName: "DietaryRequirements");

            migrationBuilder.RenameIndex(
                name: "IX_PotluckItem_ParticipantId",
                table: "PotluckItem",
                newName: "IX_PotluckItem_ContributorId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PotluckItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PotluckItem_AspNetUsers_ContributorId",
                table: "PotluckItem",
                column: "ContributorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
