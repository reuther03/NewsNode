using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.Recommendations.Database.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileRecommendation_Recommendations_Id",
                schema: "recommendations",
                table: "UserProfileRecommendation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfileRecommendation",
                schema: "recommendations",
                table: "UserProfileRecommendation");

            migrationBuilder.RenameTable(
                name: "UserProfileRecommendation",
                schema: "recommendations",
                newName: "UserProfileRecommendations",
                newSchema: "recommendations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfileRecommendations",
                schema: "recommendations",
                table: "UserProfileRecommendations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileRecommendations_Recommendations_Id",
                schema: "recommendations",
                table: "UserProfileRecommendations",
                column: "Id",
                principalSchema: "recommendations",
                principalTable: "Recommendations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileRecommendations_Recommendations_Id",
                schema: "recommendations",
                table: "UserProfileRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfileRecommendations",
                schema: "recommendations",
                table: "UserProfileRecommendations");

            migrationBuilder.RenameTable(
                name: "UserProfileRecommendations",
                schema: "recommendations",
                newName: "UserProfileRecommendation",
                newSchema: "recommendations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfileRecommendation",
                schema: "recommendations",
                table: "UserProfileRecommendation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileRecommendation_Recommendations_Id",
                schema: "recommendations",
                table: "UserProfileRecommendation",
                column: "Id",
                principalSchema: "recommendations",
                principalTable: "Recommendations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
