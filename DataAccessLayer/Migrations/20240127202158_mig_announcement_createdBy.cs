using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_announcement_createdBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AspNetUsers_AppUserId",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Announcements",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_AppUserId",
                table: "Announcements",
                newName: "IX_Announcements_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AspNetUsers_CreatedById",
                table: "Announcements",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AspNetUsers_CreatedById",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Announcements",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_CreatedById",
                table: "Announcements",
                newName: "IX_Announcements_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AspNetUsers_AppUserId",
                table: "Announcements",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
