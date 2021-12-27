using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPlanner.Migrations
{
    public partial class RSVPModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVRs_Users_UserId",
                table: "RSVRs");

            migrationBuilder.DropForeignKey(
                name: "FK_RSVRs_Weddings_WeddingWedId",
                table: "RSVRs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RSVRs",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "WeddingWedId",
                table: "RSVRs",
                newName: "WeddingID");

            migrationBuilder.RenameIndex(
                name: "IX_RSVRs_UserId",
                table: "RSVRs",
                newName: "IX_RSVRs_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_RSVRs_WeddingWedId",
                table: "RSVRs",
                newName: "IX_RSVRs_WeddingID");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVRs_Users_UserID",
                table: "RSVRs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVRs_Weddings_WeddingID",
                table: "RSVRs",
                column: "WeddingID",
                principalTable: "Weddings",
                principalColumn: "WedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVRs_Users_UserID",
                table: "RSVRs");

            migrationBuilder.DropForeignKey(
                name: "FK_RSVRs_Weddings_WeddingID",
                table: "RSVRs");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "RSVRs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "WeddingID",
                table: "RSVRs",
                newName: "WeddingWedId");

            migrationBuilder.RenameIndex(
                name: "IX_RSVRs_UserID",
                table: "RSVRs",
                newName: "IX_RSVRs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RSVRs_WeddingID",
                table: "RSVRs",
                newName: "IX_RSVRs_WeddingWedId");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVRs_Users_UserId",
                table: "RSVRs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVRs_Weddings_WeddingWedId",
                table: "RSVRs",
                column: "WeddingWedId",
                principalTable: "Weddings",
                principalColumn: "WedId");
        }
    }
}
