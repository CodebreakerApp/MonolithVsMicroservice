using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreaker.Services.Report.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MoveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    field_type = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Shape = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fields_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KeyPegs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    MoveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyPegs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyPegs_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_Color",
                table: "Fields",
                column: "Color");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_GameId",
                table: "Fields",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_Id_Position",
                table: "Fields",
                columns: new[] { "Id", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fields_MoveId",
                table: "Fields",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_Shape",
                table: "Fields",
                column: "Shape");

            migrationBuilder.CreateIndex(
                name: "IX_Games_End",
                table: "Games",
                column: "End");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Start",
                table: "Games",
                column: "Start");

            migrationBuilder.CreateIndex(
                name: "IX_Games_State",
                table: "Games",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Type",
                table: "Games",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_KeyPegs_Color",
                table: "KeyPegs",
                column: "Color");

            migrationBuilder.CreateIndex(
                name: "IX_KeyPegs_Id_Position",
                table: "KeyPegs",
                columns: new[] { "Id", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyPegs_MoveId",
                table: "KeyPegs",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_GameId",
                table: "Moves",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "KeyPegs");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
