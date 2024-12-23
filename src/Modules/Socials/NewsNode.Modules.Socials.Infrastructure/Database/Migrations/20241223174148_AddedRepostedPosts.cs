using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedRepostedPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_profile_reposted_posts",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_profile_reposted_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_profile_reposted_posts_User_profiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalSchema: "socials",
                        principalTable: "User_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_profile_reposted_posts_UserProfileId",
                schema: "socials",
                table: "User_profile_reposted_posts",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_profile_reposted_posts",
                schema: "socials");
        }
    }
}
