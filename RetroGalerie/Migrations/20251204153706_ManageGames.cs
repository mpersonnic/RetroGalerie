using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroGalerie.Migrations
{
    /// <inheritdoc />
    public partial class ManageGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsoleName",
                table: "GameViewModel");

            migrationBuilder.AddColumn<int>(
                name: "ConsoleId",
                table: "GameViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsoleId",
                table: "GameViewModel");

            migrationBuilder.AddColumn<string>(
                name: "ConsoleName",
                table: "GameViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
