using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_announcement_shdw_props : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Announcements",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 1, 31, 13, 1, 8, 937, DateTimeKind.Local).AddTicks(3009),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Edited",
                table: "Announcements",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 1, 31, 13, 1, 8, 937, DateTimeKind.Local).AddTicks(6345));

            migrationBuilder.AddColumn<string>(
                name: "EditedBy",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "EditedBy",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Announcements",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 1, 31, 13, 1, 8, 937, DateTimeKind.Local).AddTicks(3009));
        }
    }
}
