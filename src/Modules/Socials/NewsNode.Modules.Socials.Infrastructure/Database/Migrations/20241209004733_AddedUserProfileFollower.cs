using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserProfileFollower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_followers",
                schema: "socials");

            migrationBuilder.DropTable(
                name: "User_muted_profiles",
                schema: "socials");

            migrationBuilder.CreateTable(
                name: "User_profile_followers",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FollowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsUserProfileMuted = table.Column<bool>(type: "boolean", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_profile_followers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_profile_followers_User_profiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalSchema: "socials",
                        principalTable: "User_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_profile_followers_UserProfileId",
                schema: "socials",
                table: "User_profile_followers",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_profile_followers",
                schema: "socials");

            migrationBuilder.CreateTable(
                name: "User_followers",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    FollowerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_followers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_followers_User_profiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalSchema: "socials",
                        principalTable: "User_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_muted_profiles",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    MutedUserProfileId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_muted_profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_muted_profiles_User_profiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalSchema: "socials",
                        principalTable: "User_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_followers_UserProfileId",
                schema: "socials",
                table: "User_followers",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_User_muted_profiles_UserProfileId",
                schema: "socials",
                table: "User_muted_profiles",
                column: "UserProfileId");
        }
    }
}
