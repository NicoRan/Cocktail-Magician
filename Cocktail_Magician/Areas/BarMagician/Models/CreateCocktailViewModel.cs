using System.Collections.Generic;

namespace Cocktail_Magician.Areas.BarMagician.Models
{
    public class CreateCocktailViewModel
    {
        public CocktailViewModel Cocktail { get; set; }
       
        public List<string> Ingredients { get; set; }
    }
}
