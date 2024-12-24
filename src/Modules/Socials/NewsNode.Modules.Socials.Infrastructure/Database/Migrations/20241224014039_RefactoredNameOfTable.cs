using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredNameOfTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_actions_User_profiles_UserProfileId",
                schema: "socials",
                table: "post_actions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_post_actions",
                schema: "socials",
                table: "post_actions");

            migrationBuilder.RenameTable(
                name: "post_actions",
                schema: "socials",
                newName: "Post_actions",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_post_actions_UserProfileId",
                schema: "socials",
                table: "Post_actions",
                newName: "IX_Post_actions_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post_actions",
                schema: "socials",
                table: "Post_actions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_actions_User_profiles_UserProfileId",
                schema: "socials",
                table: "Post_actions",
                column: "UserProfileId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_actions_User_profiles_UserProfileId",
                schema: "socials",
                table: "Post_actions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post_actions",
                schema: "socials",
                table: "Post_actions");

            migrationBuilder.RenameTable(
                name: "Post_actions",
                schema: "socials",
                newName: "post_actions",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_Post_actions_UserProfileId",
                schema: "socials",
                table: "post_actions",
                newName: "IX_post_actions_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_post_actions",
                schema: "socials",
                table: "post_actions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_actions_User_profiles_UserProfileId",
                schema: "socials",
                table: "post_actions",
                column: "UserProfileId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
