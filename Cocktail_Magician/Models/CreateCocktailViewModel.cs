using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class CreateCocktailViewModel
    {
        public CocktailViewModel Cocktail { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
