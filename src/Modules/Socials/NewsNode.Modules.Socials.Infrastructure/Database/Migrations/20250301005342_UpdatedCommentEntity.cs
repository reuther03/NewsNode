using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCommentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ContentImg_ContentImgId",
                schema: "socials",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ContentImgId",
                schema: "socials",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ContentImgId",
                schema: "socials",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ContentImg_Id",
                schema: "socials",
                table: "Comments",
                column: "Id",
                principalSchema: "socials",
                principalTable: "ContentImg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ContentImg_Id",
                schema: "socials",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentImgId",
                schema: "socials",
                table: "Comments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ContentImgId",
                schema: "socials",
                table: "Comments",
                column: "ContentImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ContentImg_ContentImgId",
                schema: "socials",
                table: "Comments",
                column: "ContentImgId",
                principalSchema: "socials",
                principalTable: "ContentImg",
                principalColumn: "Id");
        }
    }
}
