using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocktail_Magician_Services.Mappers
{
    public static class CocktailReviewDTOMapper
    {
        public static CocktailReviewDTO ToDTO(this CocktailReview cocktailReview)
        {
            var cocktailReviewDTO = new CocktailReviewDTO
            {
                UserId = cocktailReview.UserId,
                CocktailId = cocktailReview.CocktailId,
                Grade = cocktailReview.Grade,
                Comment = cocktailReview.Comment
            };
            return cocktailReviewDTO;
        }

        public static CocktailReview ToEntity(this CocktailReviewDTO cocktailReviewDTO)
        {
            var cocktailReview = new CocktailReview
            {
                UserId = cocktailReviewDTO.UserId,
                CocktailId = cocktailReviewDTO.CocktailId,
                Grade = cocktailReviewDTO.Grade,
                Comment = cocktailReviewDTO.Comment,
            };
            return cocktailReview;
        }


        public static ICollection<CocktailReviewDTO> ToDTO(this ICollection<CocktailReview> barRatings)
        {
            var newCollection = barRatings.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }

    }
}
