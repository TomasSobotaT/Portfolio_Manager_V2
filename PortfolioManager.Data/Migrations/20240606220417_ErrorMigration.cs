using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class ErrorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AspNetUsers_UserId",
                table: "ErrorLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_UserId",
                table: "ErrorLogs");

            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "ErrorLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_UserEntityId",
                table: "ErrorLogs",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_AspNetUsers_UserEntityId",
                table: "ErrorLogs",
                column: "UserEntityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AspNetUsers_UserEntityId",
                table: "ErrorLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_UserEntityId",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "ErrorLogs");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_UserId",
                table: "ErrorLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_AspNetUsers_UserId",
                table: "ErrorLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
