using Cocktail_Magician.Areas.BarMagician.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class TopRatedHomePageViewModel
    {
        public List<CocktailViewModel> TopCocktails { get; set; }
        public List<BarViewModel> TopBars { get; set; }
    }
}
