using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedFollowersAndFollowingNameing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_following_user_profiles_UserProfileId1",
                schema: "socials",
                table: "user_following");

            migrationBuilder.DropIndex(
                name: "IX_user_following_UserProfileId1",
                schema: "socials",
                table: "user_following");

            migrationBuilder.RenameColumn(
                name: "UserProfileId1",
                schema: "socials",
                table: "user_following",
                newName: "FollowingProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_user_following_UserProfileId",
                schema: "socials",
                table: "user_following",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_following_user_profiles_UserProfileId",
                schema: "socials",
                table: "user_following",
                column: "UserProfileId",
                principalSchema: "socials",
                principalTable: "user_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_following_user_profiles_UserProfileId",
                schema: "socials",
                table: "user_following");

            migrationBuilder.DropIndex(
                name: "IX_user_following_UserProfileId",
                schema: "socials",
                table: "user_following");

            migrationBuilder.RenameColumn(
                name: "FollowingProfileId",
                schema: "socials",
                table: "user_following",
                newName: "UserProfileId1");

            migrationBuilder.CreateIndex(
                name: "IX_user_following_UserProfileId1",
                schema: "socials",
                table: "user_following",
                column: "UserProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_user_following_user_profiles_UserProfileId1",
                schema: "socials",
                table: "user_following",
                column: "UserProfileId1",
                principalSchema: "socials",
                principalTable: "user_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
