using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_baseEntity_whole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Notifications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "NewsLetters",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MessageID",
                table: "Messages",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Contacts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CommentID",
                table: "Comments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "Assignments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ArticleID",
                table: "Articles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Announcements",
                newName: "Id");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Notifications",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Notes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "NewsLetters",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "NewsLetters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "NewsLetters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Messages",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Contacts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Comments",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Categories",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Assignments",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Articles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Announcements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Announcements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Announcements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
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
                name: "Created",
                table: "NewsLetters");

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
                name: "Created",
                table: "Categories");

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

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Notifications",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "NewsLetters",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Messages",
                newName: "MessageID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contacts",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "CommentID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assignments",
                newName: "AssignmentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Articles",
                newName: "ArticleID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Announcements",
                newName: "ID");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "NewsLetters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Edited",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Edited",
                table: "Articles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Announcements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Announcements",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "Edited",
                table: "Announcements",
                type: "datetime2",
                nullable: true);
        }
    }
}
