using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_Magician_Services.Mappers
{
    public static class CocktailIngredientDTOMapper
    {
        public static CocktailIngredientDTO ToDTO(this CocktailIngredient cocktailIngredient)
        {
            var cocktailIngredientDTO = new CocktailIngredientDTO
            {
                CocktailId = cocktailIngredient.CocktailId,
                IngredientId = cocktailIngredient.IngredientId
            };
            return cocktailIngredientDTO;
        }

        public static ICollection<CocktailIngredientDTO> ToDTO(this ICollection<CocktailIngredient> cocktailIngredients)
        {
            var newCollection = cocktailIngredients.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
