using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.Recommendations.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserProfileRecommendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfileRecommendations",
                schema: "recommendations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfileRecommendations",
                schema: "recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileRecommendations_Recommendations_Id",
                        column: x => x.Id,
                        principalSchema: "recommendations",
                        principalTable: "Recommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
