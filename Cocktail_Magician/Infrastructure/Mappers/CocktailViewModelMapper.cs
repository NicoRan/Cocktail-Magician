using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
