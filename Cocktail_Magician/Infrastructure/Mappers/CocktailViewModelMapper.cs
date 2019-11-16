using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_DB.Models;
using System.Collections.Generic;

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
                Rating = cocktail.Rating
            };
            foreach (var ingredient in cocktail.Ingredients)
            {
                cocktailViewModel.Ingredients.Add(ingredient.Name);
            }
            return cocktailViewModel;
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
