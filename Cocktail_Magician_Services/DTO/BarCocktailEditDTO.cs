using Cocktail_Magician_DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_Services.DTO
{
    public class BarCocktailEditDTO
    {
        public string BarId { get; set; }
        public Bar Bar { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
