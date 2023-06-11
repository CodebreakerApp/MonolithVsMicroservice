using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreaker.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Games",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Games_End",
                table: "Games",
                column: "End");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Start",
                table: "Games",
                column: "Start");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Type",
                table: "Games",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_End",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Start",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Type",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
