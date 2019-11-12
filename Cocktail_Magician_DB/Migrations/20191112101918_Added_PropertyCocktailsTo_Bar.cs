using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocktail_Magician_DB.Migrations
{
    public partial class Added_PropertyCocktailsTo_Bar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
