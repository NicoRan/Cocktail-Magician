using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class Cocktail
    {
        public Cocktail()
        {

        }
        public Cocktail(string name)
        {
            this.Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredient { get; set; }
        public ICollection<CocktailComment> CocktailComments { get; set; }
        public ICollection<CocktailRating> CocktailRatings { get; set; }
    }
}
