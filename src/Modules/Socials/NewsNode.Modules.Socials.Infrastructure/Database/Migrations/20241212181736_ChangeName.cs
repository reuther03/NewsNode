using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_profile_followers_User_profiles_TargetUserId",
                schema: "socials",
                table: "User_profile_followers");

            migrationBuilder.DropForeignKey(
                name: "FK_User_profile_followers_User_profiles_UserId",
                schema: "socials",
                table: "User_profile_followers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_profile_followers",
                schema: "socials",
                table: "User_profile_followers");

            migrationBuilder.RenameTable(
                name: "User_profile_followers",
                schema: "socials",
                newName: "User_profile_relations",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_User_profile_followers_UserId",
                schema: "socials",
                table: "User_profile_relations",
                newName: "IX_User_profile_relations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_profile_followers_TargetUserId",
                schema: "socials",
                table: "User_profile_relations",
                newName: "IX_User_profile_relations_TargetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_profile_relations",
                schema: "socials",
                table: "User_profile_relations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_profile_relations_User_profiles_TargetUserId",
                schema: "socials",
                table: "User_profile_relations",
                column: "TargetUserId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_profile_relations_User_profiles_UserId",
                schema: "socials",
                table: "User_profile_relations",
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
                name: "FK_User_profile_relations_User_profiles_TargetUserId",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.DropForeignKey(
                name: "FK_User_profile_relations_User_profiles_UserId",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_profile_relations",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.RenameTable(
                name: "User_profile_relations",
                schema: "socials",
                newName: "User_profile_followers",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_User_profile_relations_UserId",
                schema: "socials",
                table: "User_profile_followers",
                newName: "IX_User_profile_followers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_profile_relations_TargetUserId",
                schema: "socials",
                table: "User_profile_followers",
                newName: "IX_User_profile_followers_TargetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_profile_followers",
                schema: "socials",
                table: "User_profile_followers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_profile_followers_User_profiles_TargetUserId",
                schema: "socials",
                table: "User_profile_followers",
                column: "TargetUserId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_profile_followers_User_profiles_UserId",
                schema: "socials",
                table: "User_profile_followers",
                column: "UserId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
