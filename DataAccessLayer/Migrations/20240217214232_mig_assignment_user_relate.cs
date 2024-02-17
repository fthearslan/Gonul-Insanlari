using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_assignment_user_relate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserAssignment",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssignment", x => new { x.UserId, x.AssignmentId });
                    table.ForeignKey(
                        name: "FK_UserAssignment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserAssignment_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_PublisherId",
                table: "Assignments",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignment_AssignmentId",
                table: "UserAssignment",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_PublisherId",
                table: "Assignments",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_PublisherId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "UserAssignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_PublisherId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Assignments");
        }
    }
}
