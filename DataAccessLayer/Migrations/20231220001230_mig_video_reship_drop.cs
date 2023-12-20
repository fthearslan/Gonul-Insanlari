using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_video_reship_drop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Videos_VideoID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_AppUserId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categories_CategoryID",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_CategoryID",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Comments_VideoID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoID",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Videos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsUrl",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.DropColumn(
                name: "IsUrl",
                table: "Videos");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
