using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroGalerie.Migrations
{
    /// <inheritdoc />
    public partial class AddConsoleImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_ConsoleViewModel_ConsoleViewModelId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenshots_GameViewModel_GameViewModelId",
                table: "Screenshots");

            migrationBuilder.DropTable(
                name: "ConsoleViewModel");

            migrationBuilder.DropTable(
                name: "GameViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Screenshots_GameViewModelId",
                table: "Screenshots");

            migrationBuilder.DropIndex(
                name: "IX_Games_ConsoleViewModelId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameViewModelId",
                table: "Screenshots");

            migrationBuilder.DropColumn(
                name: "ConsoleViewModelId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Consoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Consoles");

            migrationBuilder.AddColumn<int>(
                name: "GameViewModelId",
                table: "Screenshots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConsoleViewModelId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConsoleViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsoleViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsoleId = table.Column<int>(type: "int", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPlayers = table.Column<int>(type: "int", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfPublication = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screenshots_GameViewModelId",
                table: "Screenshots",
                column: "GameViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ConsoleViewModelId",
                table: "Games",
                column: "ConsoleViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_ConsoleViewModel_ConsoleViewModelId",
                table: "Games",
                column: "ConsoleViewModelId",
                principalTable: "ConsoleViewModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Screenshots_GameViewModel_GameViewModelId",
                table: "Screenshots",
                column: "GameViewModelId",
                principalTable: "GameViewModel",
                principalColumn: "Id");
        }
    }
}
