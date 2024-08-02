using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_contact_Replied_dropConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Contacts_RepliedToId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_RepliedToId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "RepliedToId",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepliedToId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RepliedToId",
                table: "Contacts",
                column: "RepliedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Contacts_RepliedToId",
                table: "Contacts",
                column: "RepliedToId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }
    }
}
