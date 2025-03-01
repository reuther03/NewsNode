using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostImg",
                schema: "socials");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentImgId",
                schema: "socials",
                table: "Comments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContentImg",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentImg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentImg_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "socials",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ContentImgId",
                schema: "socials",
                table: "Comments",
                column: "ContentImgId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentImg_PostId",
                schema: "socials",
                table: "ContentImg",
                column: "PostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ContentImg_ContentImgId",
                schema: "socials",
                table: "Comments",
                column: "ContentImgId",
                principalSchema: "socials",
                principalTable: "ContentImg",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ContentImg_ContentImgId",
                schema: "socials",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "ContentImg",
                schema: "socials");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ContentImgId",
                schema: "socials",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ContentImgId",
                schema: "socials",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "PostImg",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostImg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostImg_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "socials",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostImg_PostId",
                schema: "socials",
                table: "PostImg",
                column: "PostId",
                unique: true);
        }
    }
}
