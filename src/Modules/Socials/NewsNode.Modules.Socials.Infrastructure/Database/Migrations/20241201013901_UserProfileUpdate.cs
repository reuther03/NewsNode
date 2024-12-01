using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Test",
                schema: "socials",
                table: "user_profiles",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "socials",
                table: "user_profiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_user_profiles_Email",
                schema: "socials",
                table: "user_profiles",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_profiles_Email",
                schema: "socials",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "socials",
                table: "user_profiles");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "socials",
                table: "user_profiles",
                newName: "Test");
        }
    }
}
