using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsNode.Services.Notifications.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedAt",
                schema: "notifications",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                schema: "notifications",
                table: "Notifications",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                schema: "notifications",
                table: "Notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedAt",
                schema: "notifications",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
