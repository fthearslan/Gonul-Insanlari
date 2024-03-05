using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_baseEntity_Modified_true : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "NewsLetters",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Announcements",
                type: "datetime2",
                nullable: false,
                defaultValue: null);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Abouts",
                type: "datetime2",
                nullable: false,
                defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "NewsLetters");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Abouts");
        }
    }
}
