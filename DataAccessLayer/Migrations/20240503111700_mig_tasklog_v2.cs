using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_tasklog_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TaskLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskLogs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "TaskLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "TaskLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "TaskLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskLogs_CreatedById",
                table: "TaskLogs",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLogs_AspNetUsers_CreatedById",
                table: "TaskLogs",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLogs_AspNetUsers_CreatedById",
                table: "TaskLogs");

            migrationBuilder.DropIndex(
                name: "IX_TaskLogs_CreatedById",
                table: "TaskLogs");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "TaskLogs");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "TaskLogs");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "TaskLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TaskLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
