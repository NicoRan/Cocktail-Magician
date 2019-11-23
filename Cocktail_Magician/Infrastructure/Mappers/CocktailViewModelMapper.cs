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
        public static CocktailViewModel MapCocktailViewModel(this Cocktail cocktail)
        {
            var cocktailViewModel = new CocktailViewModel()
            {
                CocktailId = cocktail.Id,
                Information = cocktail.Information,
                Ingredients = new List<string>(),
                Name = cocktail.Name,
                Picture = cocktail.Picture,
                Rating = cocktail.CocktailReviews.Any(c => c.CocktailId == cocktail.Id) ? cocktail.CocktailReviews.Average(c => c.Grade) : 0
                
            };
            //foreach (var ingredient in cocktail.Ingredients)
            //{
            //    cocktailViewModel.Ingredients.Add(ingredient.Name);
            //}
            return cocktailViewModel;
        }

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

        public static Cocktail MapCocktail(this CocktailViewModel cocktailView)
        {
            var cocktail = new Cocktail()
            {
                Id = cocktailView.CocktailId,
                Information = cocktailView.Information,
                Name = cocktailView.Name,
                Picture = cocktailView.Picture,
                Rating = cocktailView.Rating
            };

            return cocktail;
        }
    }
}
