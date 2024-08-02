using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_ContactAttachment_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepliedToId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactAttachments_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RepliedToId",
                table: "Contacts",
                column: "RepliedToId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAttachments_ContactId",
                table: "ContactAttachments",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Contacts_RepliedToId",
                table: "Contacts",
                column: "RepliedToId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Contacts_RepliedToId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactAttachments");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_RepliedToId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "RepliedToId",
                table: "Contacts");
        }
    }
}
