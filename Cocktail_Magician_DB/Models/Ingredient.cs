using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class Ingredient
    {
        public string IngredientId { get; set; }
        public string Name { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredient { get; set; }
    }
}
