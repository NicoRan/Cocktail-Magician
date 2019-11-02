using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class Ingredient
    {
        public Ingredient()
        {

        }
        public Ingredient(string name)
        {
            this.Name = name;
        }
        public string IngredientId { get; set; }
        public string Name { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredient { get; set; }
    }
}
