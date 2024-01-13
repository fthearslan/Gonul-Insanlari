using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_article_video_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleID",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ArticleID",
                table: "Videos",
                column: "ArticleID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Articles_ArticleID",
                table: "Videos",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
