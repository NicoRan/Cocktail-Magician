using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class BarCocktail
    {
        public string BarId { get; set; }
        public Bar Bar { get; set; }

        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
