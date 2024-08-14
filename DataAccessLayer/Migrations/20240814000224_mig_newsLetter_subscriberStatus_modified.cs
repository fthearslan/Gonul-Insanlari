using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_newsLetter_subscriberStatus_modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubcsriberStatus",
                table: "NewsLetters",
                newName: "SubscriberStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriberStatus",
                table: "NewsLetters",
                newName: "SubcsriberStatus");
        }
    }
}
