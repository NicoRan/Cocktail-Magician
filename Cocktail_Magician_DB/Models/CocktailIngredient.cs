namespace Cocktail_Magician_DB.Models
{
    public class CocktailIngredient
    {
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public string IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
