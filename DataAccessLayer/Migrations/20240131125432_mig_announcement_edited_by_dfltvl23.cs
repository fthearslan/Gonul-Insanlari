using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_announcement_edited_by_dfltvl23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Edited",
                table: "Announcements",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 1, 31, 13, 54, 32, 566, DateTimeKind.Local).AddTicks(1157),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 1, 31, 13, 1, 8, 937, DateTimeKind.Local).AddTicks(6345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Announcements",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 1, 31, 13, 54, 32, 565, DateTimeKind.Local).AddTicks(8783),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 1, 31, 13, 1, 8, 937, DateTimeKind.Local).AddTicks(3009));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Edited",
                table: "Announcements",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 1, 31, 13, 1, 8, 937, DateTimeKind.Local).AddTicks(6345),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 1, 31, 13, 54, 32, 566, DateTimeKind.Local).AddTicks(1157));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Announcements",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 1, 31, 13, 1, 8, 937, DateTimeKind.Local).AddTicks(3009),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 1, 31, 13, 54, 32, 565, DateTimeKind.Local).AddTicks(8783));
        }
    }
}
