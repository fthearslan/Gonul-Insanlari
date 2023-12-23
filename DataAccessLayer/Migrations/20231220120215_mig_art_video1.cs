using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_art_video1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Articles_ArticleID",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ArticleID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ArticleID",
                table: "Videos");

            migrationBuilder.CreateTable(
                name: "ArticleVideo",
                columns: table => new
                {
                    ArticlesArticleID = table.Column<int>(type: "int", nullable: false),
                    VideosVideoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleVideo", x => new { x.ArticlesArticleID, x.VideosVideoID });
                    table.ForeignKey(
                        name: "FK_ArticleVideo_Articles_ArticlesArticleID",
                        column: x => x.ArticlesArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ArticleVideo_Videos_VideosVideoID",
                        column: x => x.VideosVideoID,
                        principalTable: "Videos",
                        principalColumn: "VideoID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleVideo_VideosVideoID",
                table: "ArticleVideo",
                column: "VideosVideoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleVideo");

            migrationBuilder.AddColumn<int>(
                name: "ArticleID",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ArticleID",
                table: "Videos",
                column: "ArticleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Articles_ArticleID",
                table: "Videos",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ArticleID");
        }
    }
}
