using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedFollowersAndFollowingNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_followers_user_profiles_FollowerId1",
                schema: "socials",
                table: "user_followers");

            migrationBuilder.RenameColumn(
                name: "FollowerId1",
                schema: "socials",
                table: "user_followers",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_user_followers_FollowerId1",
                schema: "socials",
                table: "user_followers",
                newName: "IX_user_followers_UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_followers_user_profiles_UserProfileId",
                schema: "socials",
                table: "user_followers",
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
                name: "FK_user_followers_user_profiles_UserProfileId",
                schema: "socials",
                table: "user_followers");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                schema: "socials",
                table: "user_followers",
                newName: "FollowerId1");

            migrationBuilder.RenameIndex(
                name: "IX_user_followers_UserProfileId",
                schema: "socials",
                table: "user_followers",
                newName: "IX_user_followers_FollowerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_user_followers_user_profiles_FollowerId1",
                schema: "socials",
                table: "user_followers",
                column: "FollowerId1",
                principalSchema: "socials",
                principalTable: "user_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
