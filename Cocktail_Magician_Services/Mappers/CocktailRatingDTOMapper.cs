using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System.Collections.Generic;
using System.Linq;

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

        public static CocktailRating ToRatingEntity(this CocktailReviewDTO barRatingDTO)
        {
            var barRating = new CocktailRating
            {
                UserId = barRatingDTO.UserId,
                CocktailId = barRatingDTO.CocktailId,
                Grade = barRatingDTO.Grade
            };
            return barRating;
        }

        public static ICollection<CocktailRatingDTO> ToDTO(this ICollection<CocktailRating> cocktailRatings)
        {
            var newCollection = cocktailRatings.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
