using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.GroupChats.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "group_chats");

            migrationBuilder.RenameTable(
                name: "Hashtag",
                schema: "Group_Chats",
                newName: "Hashtag",
                newSchema: "group_chats");

            migrationBuilder.RenameTable(
                name: "GroupChats",
                schema: "Group_Chats",
                newName: "GroupChats",
                newSchema: "group_chats");

            migrationBuilder.RenameTable(
                name: "GroupChatParticipants",
                schema: "Group_Chats",
                newName: "GroupChatParticipants",
                newSchema: "group_chats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Group_Chats");

            migrationBuilder.RenameTable(
                name: "Hashtag",
                schema: "group_chats",
                newName: "Hashtag",
                newSchema: "Group_Chats");

            migrationBuilder.RenameTable(
                name: "GroupChats",
                schema: "group_chats",
                newName: "GroupChats",
                newSchema: "Group_Chats");

            migrationBuilder.RenameTable(
                name: "GroupChatParticipants",
                schema: "group_chats",
                newName: "GroupChatParticipants",
                newSchema: "Group_Chats");
        }
    }
}
