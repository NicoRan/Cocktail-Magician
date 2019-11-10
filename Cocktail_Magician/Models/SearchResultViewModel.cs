using Cocktail_Magician.Areas.BarMagician.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class SearchResultViewModel
    {
        public string StarsRange { get; set; }
        public string Type { get; set; }
        public string Criteria { get; set; }
        public Dictionary<string, bool> Filter { get; set; }
        public List<BarViewModel> Bars { get; set; }
        public List<CocktailViewModel> Cocktails { get; set; }
    }
}
