using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NewsNode.Services.GroupChats.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedGroupChats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Group_Chats");

            migrationBuilder.CreateTable(
                name: "GroupChats",
                schema: "Group_Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatParticipants",
                schema: "Group_Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatParticipants_GroupChats_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Group_Chats",
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hashtag",
                schema: "Group_Chats",
                columns: table => new
                {
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hashtag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashtag", x => new { x.GroupChatId, x.Id });
                    table.ForeignKey(
                        name: "FK_Hashtag_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalSchema: "Group_Chats",
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatParticipants_GroupId",
                schema: "Group_Chats",
                table: "GroupChatParticipants",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChatParticipants",
                schema: "Group_Chats");

            migrationBuilder.DropTable(
                name: "Hashtag",
                schema: "Group_Chats");

            migrationBuilder.DropTable(
                name: "GroupChats",
                schema: "Group_Chats");
        }
    }
}
