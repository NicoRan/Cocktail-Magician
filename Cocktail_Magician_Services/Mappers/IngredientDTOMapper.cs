using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocktail_Magician_Services.Mappers
{
    public static class IngredientDTOMapper 
    {
        public static IngredientDTO ToDTO(this Ingredient ingredient)
        {
            var ingredientDTO = new IngredientDTO
            {
                 Id = ingredient.IngredientId,
                 Name = ingredient.Name
            };
            return ingredientDTO;
        }

        public static ICollection<IngredientDTO> ToDTO(this ICollection<Ingredient> ingredients)
        {
            var newCollection = ingredients.Select(c =>c.ToDTO()).ToList();
            return newCollection;
        }

    }
}
