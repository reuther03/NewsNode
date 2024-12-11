using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileRelations_User_profiles_UserProfileId",
                schema: "socials",
                table: "UserProfileRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfileRelations",
                schema: "socials",
                table: "UserProfileRelations");

            migrationBuilder.RenameTable(
                name: "UserProfileRelations",
                schema: "socials",
                newName: "User_profile_relations",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfileRelations_UserProfileId",
                schema: "socials",
                table: "User_profile_relations",
                newName: "IX_User_profile_relations_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_profile_relations",
                schema: "socials",
                table: "User_profile_relations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_profile_relations_User_profiles_UserProfileId",
                schema: "socials",
                table: "User_profile_relations",
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
                name: "FK_User_profile_relations_User_profiles_UserProfileId",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_profile_relations",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.RenameTable(
                name: "User_profile_relations",
                schema: "socials",
                newName: "UserProfileRelations",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_User_profile_relations_UserProfileId",
                schema: "socials",
                table: "UserProfileRelations",
                newName: "IX_UserProfileRelations_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfileRelations",
                schema: "socials",
                table: "UserProfileRelations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileRelations_User_profiles_UserProfileId",
                schema: "socials",
                table: "UserProfileRelations",
                column: "UserProfileId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
