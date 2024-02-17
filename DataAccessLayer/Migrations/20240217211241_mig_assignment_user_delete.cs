using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_assignment_user_delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_ReceiverId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_SenderId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ReceiverId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_SenderId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Assignments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ReceiverId",
                table: "Assignments",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_SenderId",
                table: "Assignments",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_ReceiverId",
                table: "Assignments",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_SenderId",
                table: "Assignments",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
