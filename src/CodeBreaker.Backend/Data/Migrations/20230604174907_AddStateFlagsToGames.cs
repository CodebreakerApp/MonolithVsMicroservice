using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreaker.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStateFlagsToGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Won",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Won",
                table: "Games");
        }
    }
}
