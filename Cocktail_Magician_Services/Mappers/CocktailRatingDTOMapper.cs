using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocktail_Magician_Services.Mappers
{
    public static class CocktailRatingDTOMapper
    {
        public static CocktailRatingDTO ToDTO(this CocktailRating cocktailRating)
        {
            var cocktailRatingDTO = new CocktailRatingDTO
            {
                UserId = cocktailRating.UserId,
                CocktailId = cocktailRating.CocktailId,
                Grade = cocktailRating.Grade
            };
            return cocktailRatingDTO;
        }

        public static ICollection<CocktailRatingDTO> ToDTO(this ICollection<CocktailRating> cocktailRatings)
        {
            var newCollection = cocktailRatings.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
