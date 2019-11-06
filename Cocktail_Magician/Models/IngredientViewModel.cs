using Cocktail_Magician_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class IngredientViewModel
    {
        public IngredientViewModel(Ingredient ingredient)
        {
            IngredientId = ingredient.IngredientId;
            Name = ingredient.Name;
        }

        public string IngredientId { get; set; }
        public string Name { get; set; }
    }
}
