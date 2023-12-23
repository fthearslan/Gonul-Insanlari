using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_video_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_AppUserId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Videos",
                newName: "AppUserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_AppUserId",
                table: "Videos",
                newName: "IX_Videos_AppUserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_AppUserIdId",
                table: "Videos",
                column: "AppUserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_AppUserIdId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "AppUserIdId",
                table: "Videos",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_AppUserIdId",
                table: "Videos",
                newName: "IX_Videos_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_AppUserId",
                table: "Videos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
