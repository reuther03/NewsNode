using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.Recommendations.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastInteraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastInteraction",
                schema: "recommendations",
                table: "Recommendations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastInteraction",
                schema: "recommendations",
                table: "Recommendations");
        }
    }
}
