using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_about_columns_dropped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "Details2",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "Details1",
                table: "Abouts",
                newName: "Details");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Abouts",
                newName: "Details1");

          

            migrationBuilder.AddColumn<string>(
                name: "Details2",
                table: "Abouts",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
