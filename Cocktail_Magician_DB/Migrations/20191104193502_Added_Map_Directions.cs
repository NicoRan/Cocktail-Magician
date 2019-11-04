using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocktail_Magician_DB.Migrations
{
    public partial class Added_Map_Directions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "Bars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapDirections",
                table: "Bars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "Bars");

            migrationBuilder.DropColumn(
                name: "MapDirections",
                table: "Bars");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AspNetUsers");
        }
    }
}
