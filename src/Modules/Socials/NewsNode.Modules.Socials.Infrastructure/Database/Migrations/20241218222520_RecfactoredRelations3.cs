using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Modules.Socials.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RecfactoredRelations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_profile_relations_User_profiles_UserId",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_profile_relations",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "socials",
                table: "User_profile_relations");

            migrationBuilder.RenameTable(
                name: "User_profile_relations",
                schema: "socials",
                newName: "User_profile_follows",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_User_profile_relations_UserId",
                schema: "socials",
                table: "User_profile_follows",
                newName: "IX_User_profile_follows_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_profile_follows",
                schema: "socials",
                table: "User_profile_follows",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "User_profile_statuses",
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
                    table.PrimaryKey("PK_User_profile_statuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_profile_statuses_User_profiles_UserId",
                        column: x => x.UserId,
                        principalSchema: "socials",
                        principalTable: "User_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_profile_statuses_UserId",
                schema: "socials",
                table: "User_profile_statuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_profile_follows_User_profiles_UserId",
                schema: "socials",
                table: "User_profile_follows",
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
                name: "FK_User_profile_follows_User_profiles_UserId",
                schema: "socials",
                table: "User_profile_follows");

            migrationBuilder.DropTable(
                name: "User_profile_statuses",
                schema: "socials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_profile_follows",
                schema: "socials",
                table: "User_profile_follows");

            migrationBuilder.RenameTable(
                name: "User_profile_follows",
                schema: "socials",
                newName: "User_profile_relations",
                newSchema: "socials");

            migrationBuilder.RenameIndex(
                name: "IX_User_profile_follows_UserId",
                schema: "socials",
                table: "User_profile_relations",
                newName: "IX_User_profile_relations_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "socials",
                table: "User_profile_relations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_profile_relations",
                schema: "socials",
                table: "User_profile_relations",
                column: "Id");

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
    }
}
