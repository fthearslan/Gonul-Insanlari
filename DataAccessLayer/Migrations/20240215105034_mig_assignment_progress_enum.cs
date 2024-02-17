using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_assignment_progress_enum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Assignments");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
