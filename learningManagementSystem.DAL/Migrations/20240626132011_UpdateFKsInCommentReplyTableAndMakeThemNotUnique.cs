using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learningManagementSystem.DAL.Migrations
{
    public partial class UpdateFKsInCommentReplyTableAndMakeThemNotUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommentReplies_UserId",
                table: "CommentReplies");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_UserId",
                table: "CommentReplies",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommentReplies_UserId",
                table: "CommentReplies");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_UserId",
                table: "CommentReplies",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }
    }
}
