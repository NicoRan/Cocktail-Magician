using Microsoft.EntityFrameworkCore.Migrations;

namespace Cocktail_Magician_DB.Migrations
{
    public partial class Added_IngredientsProperty_Cocktail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarComment_Bars_BarId",
                table: "BarComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BarComment_AspNetUsers_UserId",
                table: "BarComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BarRating_Bars_BarId",
                table: "BarRating");

            migrationBuilder.DropForeignKey(
                name: "FK_BarRating_AspNetUsers_UserId",
                table: "BarRating");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailComment_Cocktails_CocktailId",
                table: "CocktailComment");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailComment_AspNetUsers_UserId",
                table: "CocktailComment");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailRating_Cocktails_CocktailId",
                table: "CocktailRating");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailRating_AspNetUsers_UserId",
                table: "CocktailRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailRating",
                table: "CocktailRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailComment",
                table: "CocktailComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarRating",
                table: "BarRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarComment",
                table: "BarComment");

            migrationBuilder.RenameTable(
                name: "CocktailRating",
                newName: "CocktailRatings");

            migrationBuilder.RenameTable(
                name: "CocktailComment",
                newName: "CocktailComments");

            migrationBuilder.RenameTable(
                name: "BarRating",
                newName: "BarRatings");

            migrationBuilder.RenameTable(
                name: "BarComment",
                newName: "BarComments");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailRating_CocktailId",
                table: "CocktailRatings",
                newName: "IX_CocktailRatings_CocktailId");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailComment_CocktailId",
                table: "CocktailComments",
                newName: "IX_CocktailComments_CocktailId");

            migrationBuilder.RenameIndex(
                name: "IX_BarRating_BarId",
                table: "BarRatings",
                newName: "IX_BarRatings_BarId");

            migrationBuilder.RenameIndex(
                name: "IX_BarComment_BarId",
                table: "BarComments",
                newName: "IX_BarComments_BarId");

            migrationBuilder.AddColumn<string>(
                name: "CocktailId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailRatings",
                table: "CocktailRatings",
                columns: new[] { "UserId", "CocktailId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailComments",
                table: "CocktailComments",
                columns: new[] { "UserId", "CocktailId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarRatings",
                table: "BarRatings",
                columns: new[] { "UserId", "BarId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarComments",
                table: "BarComments",
                columns: new[] { "UserId", "BarId" });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_CocktailId",
                table: "Ingredients",
                column: "CocktailId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarComments_Bars_BarId",
                table: "BarComments",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "BarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarComments_AspNetUsers_UserId",
                table: "BarComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarRatings_Bars_BarId",
                table: "BarRatings",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "BarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarRatings_AspNetUsers_UserId",
                table: "BarRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailComments_Cocktails_CocktailId",
                table: "CocktailComments",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailComments_AspNetUsers_UserId",
                table: "CocktailComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailRatings_Cocktails_CocktailId",
                table: "CocktailRatings",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailRatings_AspNetUsers_UserId",
                table: "CocktailRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Cocktails_CocktailId",
                table: "Ingredients",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarComments_Bars_BarId",
                table: "BarComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BarComments_AspNetUsers_UserId",
                table: "BarComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BarRatings_Bars_BarId",
                table: "BarRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_BarRatings_AspNetUsers_UserId",
                table: "BarRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailComments_Cocktails_CocktailId",
                table: "CocktailComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailComments_AspNetUsers_UserId",
                table: "CocktailComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailRatings_Cocktails_CocktailId",
                table: "CocktailRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailRatings_AspNetUsers_UserId",
                table: "CocktailRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Cocktails_CocktailId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_CocktailId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailRatings",
                table: "CocktailRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailComments",
                table: "CocktailComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarRatings",
                table: "BarRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarComments",
                table: "BarComments");

            migrationBuilder.DropColumn(
                name: "CocktailId",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "CocktailRatings",
                newName: "CocktailRating");

            migrationBuilder.RenameTable(
                name: "CocktailComments",
                newName: "CocktailComment");

            migrationBuilder.RenameTable(
                name: "BarRatings",
                newName: "BarRating");

            migrationBuilder.RenameTable(
                name: "BarComments",
                newName: "BarComment");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailRatings_CocktailId",
                table: "CocktailRating",
                newName: "IX_CocktailRating_CocktailId");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailComments_CocktailId",
                table: "CocktailComment",
                newName: "IX_CocktailComment_CocktailId");

            migrationBuilder.RenameIndex(
                name: "IX_BarRatings_BarId",
                table: "BarRating",
                newName: "IX_BarRating_BarId");

            migrationBuilder.RenameIndex(
                name: "IX_BarComments_BarId",
                table: "BarComment",
                newName: "IX_BarComment_BarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailRating",
                table: "CocktailRating",
                columns: new[] { "UserId", "CocktailId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailComment",
                table: "CocktailComment",
                columns: new[] { "UserId", "CocktailId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarRating",
                table: "BarRating",
                columns: new[] { "UserId", "BarId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarComment",
                table: "BarComment",
                columns: new[] { "UserId", "BarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BarComment_Bars_BarId",
                table: "BarComment",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "BarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarComment_AspNetUsers_UserId",
                table: "BarComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarRating_Bars_BarId",
                table: "BarRating",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "BarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarRating_AspNetUsers_UserId",
                table: "BarRating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailComment_Cocktails_CocktailId",
                table: "CocktailComment",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailComment_AspNetUsers_UserId",
                table: "CocktailComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailRating_Cocktails_CocktailId",
                table: "CocktailRating",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailRating_AspNetUsers_UserId",
                table: "CocktailRating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
