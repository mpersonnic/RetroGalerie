using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroGalerie.Migrations
{
    /// <inheritdoc />
    public partial class GamesList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameViewModelId",
                table: "Screenshots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfPublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPlayers = table.Column<int>(type: "int", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screenshots_GameViewModelId",
                table: "Screenshots",
                column: "GameViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Screenshots_GameViewModel_GameViewModelId",
                table: "Screenshots",
                column: "GameViewModelId",
                principalTable: "GameViewModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Screenshots_GameViewModel_GameViewModelId",
                table: "Screenshots");

            migrationBuilder.DropTable(
                name: "GameViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Screenshots_GameViewModelId",
                table: "Screenshots");

            migrationBuilder.DropColumn(
                name: "GameViewModelId",
                table: "Screenshots");
        }
    }
}
