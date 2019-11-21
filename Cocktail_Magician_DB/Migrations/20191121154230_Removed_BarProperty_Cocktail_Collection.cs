using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocktail_Magician_DB.Migrations
{
    public partial class Removed_BarProperty_Cocktail_Collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cocktails_Bars_BarId",
                table: "Cocktails");

            migrationBuilder.DropIndex(
                name: "IX_Cocktails_BarId",
                table: "Cocktails");

            migrationBuilder.DropColumn(
                name: "BarId",
                table: "Cocktails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BarId",
                table: "Cocktails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cocktails_BarId",
                table: "Cocktails",
                column: "BarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cocktails_Bars_BarId",
                table: "Cocktails",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "BarId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
