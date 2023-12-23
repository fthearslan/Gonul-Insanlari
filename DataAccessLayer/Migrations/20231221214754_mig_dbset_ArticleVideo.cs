using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_dbset_ArticleVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleVideo_Articles_ArticleID",
                table: "ArticleVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleVideo_Videos_VideoID",
                table: "ArticleVideo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleVideo",
                table: "ArticleVideo");

            migrationBuilder.RenameTable(
                name: "ArticleVideo",
                newName: "ArticleVideos");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleVideo_VideoID",
                table: "ArticleVideos",
                newName: "IX_ArticleVideos_VideoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleVideos",
                table: "ArticleVideos",
                columns: new[] { "ArticleID", "VideoID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleVideos_Articles_ArticleID",
                table: "ArticleVideos",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleVideos_Videos_VideoID",
                table: "ArticleVideos",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleVideos_Articles_ArticleID",
                table: "ArticleVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleVideos_Videos_VideoID",
                table: "ArticleVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleVideos",
                table: "ArticleVideos");

            migrationBuilder.RenameTable(
                name: "ArticleVideos",
                newName: "ArticleVideo");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleVideos_VideoID",
                table: "ArticleVideo",
                newName: "IX_ArticleVideo_VideoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleVideo",
                table: "ArticleVideo",
                columns: new[] { "ArticleID", "VideoID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleVideo_Articles_ArticleID",
                table: "ArticleVideo",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleVideo_Videos_VideoID",
                table: "ArticleVideo",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
