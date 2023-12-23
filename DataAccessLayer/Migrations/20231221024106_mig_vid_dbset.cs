using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_vid_dbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_AppUserId",
                table: "Videos",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_AppUserId",
                table: "Videos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_AppUserId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_AppUserId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Videos");
        }
    }
}
