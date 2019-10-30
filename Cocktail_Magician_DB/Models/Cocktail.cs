using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class Cocktail
    {
        public string CocktailId { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        //
        public ICollection<CocktailIngredient> CocktailIngredient { get; set; }
        public bool IsDeleted { get; set; }
    }
}
