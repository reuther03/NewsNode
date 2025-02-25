using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredRelationOfPostImgToOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "socials",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                schema: "socials",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "PostImg",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostImg",
                schema: "socials");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "socials",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                schema: "socials",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
