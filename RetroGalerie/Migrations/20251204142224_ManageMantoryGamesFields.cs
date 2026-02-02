using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroGalerie.Migrations
{
    /// <inheritdoc />
    public partial class ManageMantoryGamesFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfPublication",
                table: "GameViewModel");

            migrationBuilder.DropColumn(
                name: "DateOfPublication",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "YearOfPublication",
                table: "GameViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearOfPublication",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfPublication",
                table: "GameViewModel");

            migrationBuilder.DropColumn(
                name: "YearOfPublication",
                table: "Games");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPublication",
                table: "GameViewModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPublication",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
