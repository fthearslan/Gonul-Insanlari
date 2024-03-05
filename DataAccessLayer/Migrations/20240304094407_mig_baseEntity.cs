using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_baseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Abouts",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Abouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Abouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Abouts",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "Discriminator",
                table: "Abouts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
