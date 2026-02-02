using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroGalerie.Migrations
{
    /// <inheritdoc />
    public partial class ManageConsole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoverImageUrl",
                table: "GameViewModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsoleViewModel", x => x.Id);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_ConsoleViewModel_ConsoleViewModelId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "ConsoleViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Games_ConsoleViewModelId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ConsoleViewModelId",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "CoverImageUrl",
                table: "GameViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
