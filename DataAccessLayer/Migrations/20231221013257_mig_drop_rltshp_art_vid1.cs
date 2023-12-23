using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_drop_rltshp_art_vid1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleVideo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleVideo_Videos_VideosVideoID",
                        column: x => x.VideosVideoID,
                        principalTable: "Videos",
                        principalColumn: "VideoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleVideo_VideosVideoID",
                table: "ArticleVideo",
                column: "VideosVideoID");
        }
    }
}
