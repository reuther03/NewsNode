using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.Recommendations.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserProfileRecommendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hashtag",
                schema: "recommendations",
                table: "Recommendations");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "recommendations",
                table: "Recommendations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Hashtag",
                schema: "recommendations",
                table: "CountryRecommendations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hashtag",
                schema: "recommendations",
                table: "ActionRecommendations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserProfileRecommendation",
                schema: "recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileRecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileRecommendation_Recommendations_Id",
                        column: x => x.Id,
                        principalSchema: "recommendations",
                        principalTable: "Recommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfileRecommendation",
                schema: "recommendations");

            migrationBuilder.DropColumn(
                name: "Hashtag",
                schema: "recommendations",
                table: "CountryRecommendations");

            migrationBuilder.DropColumn(
                name: "Hashtag",
                schema: "recommendations",
                table: "ActionRecommendations");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "recommendations",
                table: "Recommendations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Hashtag",
                schema: "recommendations",
                table: "Recommendations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
