using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeenPostToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SeenPosts_UserId",
                schema: "socials",
                table: "SeenPosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeenPosts_User_profiles_UserId",
                schema: "socials",
                table: "SeenPosts",
                column: "UserId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeenPosts_User_profiles_UserId",
                schema: "socials",
                table: "SeenPosts");

            migrationBuilder.DropIndex(
                name: "IX_SeenPosts_UserId",
                schema: "socials",
                table: "SeenPosts");
        }
    }
}
