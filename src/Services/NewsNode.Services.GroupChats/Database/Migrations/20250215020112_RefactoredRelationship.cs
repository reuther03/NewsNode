using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.GroupChats.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_GroupUsers_SenderId",
                schema: "group_chats",
                table: "ChatMessages");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_GroupUsers_SenderId",
                schema: "group_chats",
                table: "ChatMessages",
                column: "SenderId",
                principalSchema: "group_chats",
                principalTable: "GroupUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_GroupUsers_SenderId",
                schema: "group_chats",
                table: "ChatMessages");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_GroupUsers_SenderId",
                schema: "group_chats",
                table: "ChatMessages",
                column: "SenderId",
                principalSchema: "group_chats",
                principalTable: "GroupUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
