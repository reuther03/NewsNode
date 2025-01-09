using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.Recommendations.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "recommendations");

            migrationBuilder.CreateTable(
                name: "Recommendations",
                schema: "recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Hashtag = table.Column<string>(type: "text", nullable: false),
                    LastInteraction = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionRecommendations",
                schema: "recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionRecommendations_Recommendations_Id",
                        column: x => x.Id,
                        principalSchema: "recommendations",
                        principalTable: "Recommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryRecommendations",
                schema: "recommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryRecommendations_Recommendations_Id",
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
                name: "ActionRecommendations",
                schema: "recommendations");

            migrationBuilder.DropTable(
                name: "CountryRecommendations",
                schema: "recommendations");

            migrationBuilder.DropTable(
                name: "Recommendations",
                schema: "recommendations");
        }
    }
}
