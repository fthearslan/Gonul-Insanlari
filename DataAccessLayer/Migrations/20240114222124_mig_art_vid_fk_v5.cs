using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_art_vid_fk_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Articles_Id",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Videos",
                newName: "ArticleID");

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "VideoId");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ArticleID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "ArticleID",
                table: "Videos",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Articles_Id",
                table: "Videos",
                column: "Id",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
