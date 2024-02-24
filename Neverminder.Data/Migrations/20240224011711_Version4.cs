using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neverminder.Data.Migrations
{
    /// <inheritdoc />
    public partial class Version4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Reminders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Reminders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_PlatformId",
                table: "Reminders",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Platforms_PlatformId",
                table: "Reminders",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Platforms_PlatformId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_PlatformId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Reminders");
        }
    }
}
