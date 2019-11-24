using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class CocktailViewModelMapper
    {
        public static CocktailViewModel ToVM(this CocktailDTO cocktail)
        {
            var cocktailViewModel = new CocktailViewModel()
            {
                CocktailId = cocktail.Id,
                Information = cocktail.Information,
                Name = cocktail.Name,
                Picture = cocktail.Picture,
                Rating = cocktail.Rating,
                ReviewViewModels = cocktail.CocktailReviewDTOs.ToCocktailReviewVM()
            };
            return cocktailViewModel;
        }

        public static CocktailViewModel ToCatalogVM(this CocktailDTO cocktail)
        {
            var cocktailViewModel = new CocktailViewModel()
            {
                CocktailId = cocktail.Id,
                Information = cocktail.Information,
                Name = cocktail.Name,
                Picture = cocktail.Picture,
                Rating = cocktail.Rating
            };
            return cocktailViewModel;
        }

        public static CocktailDTO ToDTO(this CocktailViewModel cocktailView)
        {
            var cocktailViewModel = new CocktailDTO()
            {
                //Id = cocktailView.CocktailId,
                //Information = cocktailView.Information,
                Name = cocktailView.Name,
                Picture = cocktailView.Picture,
                Rating = cocktailView.Rating,
                CocktailReviewDTOs = cocktailView.ReviewViewModels?.ToCocktailReviewDTO()
            };
            return cocktailViewModel;
        }

        public static ICollection<CocktailViewModel> ToVM(this ICollection<CocktailDTO> cocktails)
        {
            var newCollection = cocktails.Select(c => c.ToVM()).ToList();
            return newCollection;
        }

        public static ICollection<CocktailViewModel> ToCatalogVM(this ICollection<CocktailDTO> cocktails)
        {
            var newCollection = cocktails.Select(c => c.ToCatalogVM()).ToList();
            return newCollection;
        }

        public static ICollection<CocktailDTO> ToDTO(this ICollection<CocktailViewModel> cocktails)
        {
            var newCollection = cocktails.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
