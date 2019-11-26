using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class CocktailIngredientViewModelMapper
    {
        public static CocktailIngredientDTO ToDTO(this CocktailIngredientsViewModel viewModel)
        {
            var cocktailIngredientDTO = new CocktailIngredientDTO
            {
                CocktailId = viewModel.CocktailId,
                IngredientId = viewModel.IngredientId,
                IngredientName = viewModel.IngredientName
            };
            return cocktailIngredientDTO;
        }

        public static CocktailIngredientsViewModel ToVM(this CocktailIngredientDTO cocktailIngredientDTO)
        {
            var viewModel = new CocktailIngredientsViewModel
            {
                CocktailId = cocktailIngredientDTO.CocktailId,
                IngredientId = cocktailIngredientDTO.IngredientId,
                IngredientName = cocktailIngredientDTO.IngredientName
            };
            return viewModel;
        }

        public static ICollection<CocktailIngredientDTO> ToDTO(this ICollection<CocktailIngredientsViewModel> viewModels)
        {
            var newCollection = viewModels.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }

        public static ICollection<CocktailIngredientsViewModel> ToVM(this ICollection<CocktailIngredientDTO> cocktails)
        {
            var newCollection = cocktails.Select(c => c.ToVM()).ToList();
            return newCollection;
        }
    }
}
