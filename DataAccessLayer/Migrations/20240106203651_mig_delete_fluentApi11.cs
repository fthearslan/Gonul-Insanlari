using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_delete_fluentApi11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

           

          
           
         

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Videos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

           

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Articles",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

           

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleVideos");

            migrationBuilder.DropColumn(
                name: "IsUrl",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Articles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Videos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArticleID",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Videos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Edited",
                table: "Videos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Videos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VideoID",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_AppUserId",
                table: "Videos",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ArticleID",
                table: "Videos",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CategoryID",
                table: "Videos",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoID",
                table: "Comments",
                column: "VideoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Videos_VideoID",
                table: "Comments",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Articles_ArticleID",
                table: "Videos",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ArticleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_AppUserId",
                table: "Videos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categories_CategoryID",
                table: "Videos",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
