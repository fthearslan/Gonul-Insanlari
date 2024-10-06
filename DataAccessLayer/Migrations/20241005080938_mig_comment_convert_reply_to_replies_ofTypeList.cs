using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_comment_convert_reply_to_replies_ofTypeList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ReplyId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ReplyId",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ReplyId",
                table: "Comments",
                newName: "IX_Comments_CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "ReplyId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                newName: "IX_Comments_ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ReplyId",
                table: "Comments",
                column: "ReplyId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
