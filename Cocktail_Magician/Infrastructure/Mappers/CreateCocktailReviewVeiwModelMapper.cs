using Cocktail_Magician.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class CreateCocktailReviewVeiwModelMapper
    {
        public static CocktailReviewDTO ToCocktailDTO(this CreateReviewViewModel createReviewViewModel)
        {
            var cocktailCommentDTO = new CocktailReviewDTO
            {
                CocktailId = createReviewViewModel.Id,
                UserId = createReviewViewModel.UserId,
                Comment = createReviewViewModel.Comment,
                Grade = createReviewViewModel.Rate
            };
            return cocktailCommentDTO;
        }
    }
}
