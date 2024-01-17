using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_delete_rlt_art_appuser_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Articles",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles",
                newName: "IX_Articles_AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserID",
                table: "Articles",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserID",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "Articles",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AppUserID",
                table: "Articles",
                newName: "IX_Articles_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
