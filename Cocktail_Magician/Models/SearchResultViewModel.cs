using Cocktail_Magician.Areas.BarMagician.Models;
using System.Collections.Generic;

namespace Cocktail_Magician.Models
{
    public class SearchResultViewModel
    {
        public string StarsRange { get; set; }
        public string Type { get; set; }
        public string Criteria { get; set; }
        public string SortOptions { get; set; }
        public Dictionary<string, bool> Filter { get; set; }
        public List<BarViewModel> Bars { get; set; }
        public List<CocktailViewModel> Cocktails { get; set; }
    }
}
