using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreaker.Services.Bot.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBotTypeColumnNameAndDiscriminatorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Bots",
                newName: "BotType");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Bots",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BotType",
                table: "Bots",
                newName: "Discriminator");

            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Bots",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
