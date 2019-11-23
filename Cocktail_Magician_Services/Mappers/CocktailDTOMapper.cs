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
                Rating = cocktail.Rating,
                CocktailReviewDTOs = cocktail.CocktailReviews.ToDTO()
            };
            return cocktailDTO;
        }

        public static Cocktail ToEntity(this CocktailDTO cocktailDTO)
        {
            var cocktail = new Cocktail
            {
                Id = cocktailDTO.Id,
                Name = cocktailDTO.Name,
                Information = cocktailDTO.Information,
                Picture = cocktailDTO.Picture,
                IsDeleted = cocktailDTO.IsDeleted,
                CocktailReviews = cocktailDTO.CocktailReviewDTOs?.ToEntity()
            };
            return cocktail;
        }
        public static Cocktail ToCreateEntity(this CocktailDTO cocktailDTO)
        {
            var cocktail = new Cocktail
            {
                Name = cocktailDTO.Name,
                Picture = cocktailDTO.Picture,
                IsDeleted = cocktailDTO.IsDeleted,
                CocktailReviews = cocktailDTO.CocktailReviewDTOs?.ToEntity()
            };
            return cocktail;
        }

        public static ICollection<CocktailDTO> ToDTO(this ICollection<Cocktail> cocktails)
        {
            var newCollection = cocktails.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }

        public static ICollection<Cocktail> ToEntity(this ICollection<CocktailDTO> cocktails)
        {
            var newCollection = cocktails.Select(c => c.ToEntity()).ToList();
            return newCollection;
        }
    }
}
