using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_Magician_Services.Mappers
{
    public static class CocktailDTOMapper
    {
        public static CocktailDTO ToDTO(this Cocktail cocktail)
        {
            var cocktailDTO = new CocktailDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Information = cocktail.Information,
                Picture = cocktail.Picture,
                IsDeleted = cocktail.IsDeleted,
                Rating = cocktail.CocktailReviews.Any(c => c.CocktailId == cocktail.Id) ? cocktail.CocktailReviews.Average(c => c.Grade) : 0,
                CocktailReviewDTOs = cocktail.CocktailReviews.ToDTO()
            };
            return cocktailDTO;
        }

        public static ICollection<CocktailDTO> ToDTO(this ICollection<Cocktail> cocktails)
        {
            var newCollection = cocktails.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
