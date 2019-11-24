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
                Comment = cocktailReview.Comment,
                UserName = cocktailReview.User.UserName,
                UserPicture = cocktailReview.User.Picture,
                CreatedOn = cocktailReview.CreatedOn
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
                CreatedOn = cocktailReviewDTO.CreatedOn
            };
            return cocktailReview;
        }


        public static ICollection<CocktailReviewDTO> ToDTO(this ICollection<CocktailReview> cocktailReviews)
        {
            var newCollection = cocktailReviews.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }

        public static ICollection<CocktailReview> ToEntity(this ICollection<CocktailReviewDTO> cocktailReviewsDTO)
        {
            var newCollection = cocktailReviewsDTO.Select(c => c.ToEntity()).ToList();
            return newCollection;
        }
    }
}
