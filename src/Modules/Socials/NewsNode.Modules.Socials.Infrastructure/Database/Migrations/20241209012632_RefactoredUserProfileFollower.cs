using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredUserProfileFollower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUserProfileMuted",
                schema: "socials",
                table: "User_profile_followers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUserProfileMuted",
                schema: "socials",
                table: "User_profile_followers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
