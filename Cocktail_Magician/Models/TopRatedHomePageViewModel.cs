using Cocktail_Magician.Areas.BarMagician.Models;
using System.Collections.Generic;

namespace Cocktail_Magician.Models
{
    public class TopRatedHomePageViewModel
    {
        public ICollection<CocktailViewModel> TopCocktails { get; set; }
        public ICollection<BarViewModel> TopBars { get; set; }
    }
}
