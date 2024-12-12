using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "socials");

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PostedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Likes = table.Column<int>(type: "integer", nullable: false),
                    Bookmarks = table.Column<int>(type: "integer", nullable: false),
                    Reposts = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_profiles",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    PostedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Likes = table.Column<int>(type: "integer", nullable: false),
                    Bookmarks = table.Column<int>(type: "integer", nullable: false),
                    Reposts = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "socials",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_profile_followers",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_profile_followers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_profile_followers_User_profiles_TargetUserId",
                        column: x => x.TargetUserId,
                        principalSchema: "socials",
                        principalTable: "User_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_profile_followers_User_profiles_UserId",
                        column: x => x.UserId,
                        principalSchema: "socials",
                        principalTable: "User_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                schema: "socials",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_User_profile_followers_TargetUserId",
                schema: "socials",
                table: "User_profile_followers",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_profile_followers_UserId",
                schema: "socials",
                table: "User_profile_followers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_profiles_Email",
                schema: "socials",
                table: "User_profiles",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "socials");

            migrationBuilder.DropTable(
                name: "User_profile_followers",
                schema: "socials");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "socials");

            migrationBuilder.DropTable(
                name: "User_profiles",
                schema: "socials");
        }
    }
}
