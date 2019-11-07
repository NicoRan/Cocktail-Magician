using Cocktail_Magician.Areas.BarMagician.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class SearchResultViewModel
    {
        public List<BarViewModel> Bars { get; set; }
        public List<CocktailViewModel> Cocktails { get; set; }
    }
}
