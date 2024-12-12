using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_profile_relations_User_profiles_TargetUserId",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.DropIndex(
                name: "IX_User_profile_relations_TargetUserId",
                schema: "socials",
                table: "User_profile_relations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_profile_relations_TargetUserId",
                schema: "socials",
                table: "User_profile_relations",
                column: "TargetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_profile_relations_User_profiles_TargetUserId",
                schema: "socials",
                table: "User_profile_relations",
                column: "TargetUserId",
                principalSchema: "socials",
                principalTable: "User_profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
