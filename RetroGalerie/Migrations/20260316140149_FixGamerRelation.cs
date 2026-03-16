using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroGalerie.Migrations
{
    /// <inheritdoc />
    public partial class FixGamerRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGamers_AspNetUsers_GamerId",
                table: "GameGamers");

            migrationBuilder.DropIndex(
                name: "IX_GameGamers_GamerId",
                table: "GameGamers");

            migrationBuilder.DropColumn(
                name: "GamerId",
                table: "GameGamers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GamerId",
                table: "GameGamers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameGamers_GamerId",
                table: "GameGamers",
                column: "GamerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGamers_AspNetUsers_GamerId",
                table: "GameGamers",
                column: "GamerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
