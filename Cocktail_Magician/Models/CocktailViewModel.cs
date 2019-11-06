using Cocktail_Magician_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class CocktailViewModel
    {
        public CocktailViewModel()
        {

        }
        public CocktailViewModel(Cocktail cocktail)
        {
            CocktailId = cocktail.Id;
            Name = cocktail.Name;
            Ingredients = new List<string>();
        }

        public string CocktailId { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
