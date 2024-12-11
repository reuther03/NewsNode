using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_profile_followers_UserProfileId",
                schema: "socials",
                table: "User_profile_followers");

            migrationBuilder.CreateIndex(
                name: "IX_User_profile_followers_FollowerId",
                schema: "socials",
                table: "User_profile_followers",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_profile_followers_UserProfileId_FollowerId",
                schema: "socials",
                table: "User_profile_followers",
                columns: new[] { "UserProfileId", "FollowerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_User_profile_followers_User_profiles_FollowerId",
                schema: "socials",
                table: "User_profile_followers",
                column: "FollowerId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_profile_followers_User_profiles_FollowerId",
                schema: "socials",
                table: "User_profile_followers");

            migrationBuilder.DropIndex(
                name: "IX_User_profile_followers_FollowerId",
                schema: "socials",
                table: "User_profile_followers");

            migrationBuilder.DropIndex(
                name: "IX_User_profile_followers_UserProfileId_FollowerId",
                schema: "socials",
                table: "User_profile_followers");

            migrationBuilder.CreateIndex(
                name: "IX_User_profile_followers_UserProfileId",
                schema: "socials",
                table: "User_profile_followers",
                column: "UserProfileId");
        }
    }
}
