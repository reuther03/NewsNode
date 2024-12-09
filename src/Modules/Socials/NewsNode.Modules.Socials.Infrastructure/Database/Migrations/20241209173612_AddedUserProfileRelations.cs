using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserProfileRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfileRelations",
                schema: "socials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelationStatus = table.Column<string>(type: "text", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileRelations_User_profiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalSchema: "socials",
                        principalTable: "User_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileRelations_UserProfileId",
                schema: "socials",
                table: "UserProfileRelations",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfileRelations",
                schema: "socials");
        }
    }
}
