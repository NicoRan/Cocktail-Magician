using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocktail_Magician_Services.Mappers
{
    public static class BarCocktailDTOMapper
    {
        public static BarCocktailDTO ToDTO(this BarCocktail barCocktail)
        {
            var barCocktailDTO = new BarCocktailDTO
            {
                BarId = barCocktail.BarId,
                CocktailId = barCocktail.CocktailId
            };
            return barCocktailDTO;
        }

        public static ICollection<BarCocktailDTO> ToDTO(this ICollection<BarCocktail> barCocktails)
        {
            var newCollection = barCocktails.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
