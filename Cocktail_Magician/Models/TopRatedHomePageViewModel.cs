using Cocktail_Magician.Areas.BarMagician.Models;
using System.Collections.Generic;

namespace Cocktail_Magician.Models
{
    public class TopRatedHomePageViewModel
    {
        public List<CocktailViewModel> TopCocktails { get; set; }
        public List<BarViewModel> TopBars { get; set; }
    }
}
