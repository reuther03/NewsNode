using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class NamingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_followers_user_profiles_UserProfileId",
                schema: "socials",
                table: "user_followers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_profiles",
                schema: "socials",
                table: "user_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_followers",
                schema: "socials",
                table: "user_followers");

            migrationBuilder.RenameTable(
                name: "user_profiles",
                schema: "socials",
                newName: "User_profiles",
                newSchema: "socials");

            migrationBuilder.RenameTable(
                name: "user_followers",
                schema: "socials",
                newName: "User_followers",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_user_profiles_Email",
                schema: "socials",
                table: "User_profiles",
                newName: "IX_User_profiles_Email");

            migrationBuilder.RenameIndex(
                name: "IX_user_followers_UserProfileId",
                schema: "socials",
                table: "User_followers",
                newName: "IX_User_followers_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_profiles",
                schema: "socials",
                table: "User_profiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_followers",
                schema: "socials",
                table: "User_followers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_followers_User_profiles_UserProfileId",
                schema: "socials",
                table: "User_followers",
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
                name: "FK_User_followers_User_profiles_UserProfileId",
                schema: "socials",
                table: "User_followers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_profiles",
                schema: "socials",
                table: "User_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_followers",
                schema: "socials",
                table: "User_followers");

            migrationBuilder.RenameTable(
                name: "User_profiles",
                schema: "socials",
                newName: "user_profiles",
                newSchema: "socials");

            migrationBuilder.RenameTable(
                name: "User_followers",
                schema: "socials",
                newName: "user_followers",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_User_profiles_Email",
                schema: "socials",
                table: "user_profiles",
                newName: "IX_user_profiles_Email");

            migrationBuilder.RenameIndex(
                name: "IX_User_followers_UserProfileId",
                schema: "socials",
                table: "user_followers",
                newName: "IX_user_followers_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_profiles",
                schema: "socials",
                table: "user_profiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_followers",
                schema: "socials",
                table: "user_followers",
                column: "Id");

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
    }
}
