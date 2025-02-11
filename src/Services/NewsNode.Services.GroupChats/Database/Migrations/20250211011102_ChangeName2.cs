using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.GroupChats.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatParticipants_GroupChats_GroupId",
                schema: "group_chats",
                table: "GroupChatParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Hashtag_GroupChats_GroupChatId",
                schema: "group_chats",
                table: "Hashtag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupChats",
                schema: "group_chats",
                table: "GroupChats");

            migrationBuilder.RenameTable(
                name: "GroupChats",
                schema: "group_chats",
                newName: "group_chats",
                newSchema: "group_chats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_group_chats",
                schema: "group_chats",
                table: "group_chats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatParticipants_group_chats_GroupId",
                schema: "group_chats",
                table: "GroupChatParticipants",
                column: "GroupId",
                principalSchema: "group_chats",
                principalTable: "group_chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtag_group_chats_GroupChatId",
                schema: "group_chats",
                table: "Hashtag",
                column: "GroupChatId",
                principalSchema: "group_chats",
                principalTable: "group_chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChatParticipants_group_chats_GroupId",
                schema: "group_chats",
                table: "GroupChatParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Hashtag_group_chats_GroupChatId",
                schema: "group_chats",
                table: "Hashtag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_group_chats",
                schema: "group_chats",
                table: "group_chats");

            migrationBuilder.RenameTable(
                name: "group_chats",
                schema: "group_chats",
                newName: "GroupChats",
                newSchema: "group_chats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupChats",
                schema: "group_chats",
                table: "GroupChats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChatParticipants_GroupChats_GroupId",
                schema: "group_chats",
                table: "GroupChatParticipants",
                column: "GroupId",
                principalSchema: "group_chats",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtag_GroupChats_GroupChatId",
                schema: "group_chats",
                table: "Hashtag",
                column: "GroupChatId",
                principalSchema: "group_chats",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
