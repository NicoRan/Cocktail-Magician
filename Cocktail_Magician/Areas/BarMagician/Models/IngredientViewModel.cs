using Cocktail_Magician_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Areas.BarMagician.Models
{
    public class IngredientViewModel
    {
        public IngredientViewModel(Ingredient ingredient)
        {
            Id = ingredient.IngredientId;
            Name = ingredient.Name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
