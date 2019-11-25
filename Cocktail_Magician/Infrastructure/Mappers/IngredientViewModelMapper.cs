using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class IngredientViewModelMapper
    {
        public static IngredientViewModel ToVM(this IngredientDTO ingredientDTO)
        {
            var ingredidentViewModel = new IngredientViewModel
            {
                Id = ingredientDTO.Id,
                Name = ingredientDTO.Name
            };
            return ingredidentViewModel;
        }

        public static IngredientDTO ToDTO(this IngredientViewModel ingredientViewModel)
        {
            var ingredientDTO = new IngredientDTO
            {
                Id = ingredientViewModel.Id,
                Name = ingredientViewModel.Name
            };
            return ingredientDTO;
        }

        public static ICollection<IngredientViewModel> ToVM(this ICollection<IngredientDTO> ingredient)
        {
            var newCollection = ingredient.Select(i => i.ToVM()).ToList();
            return newCollection;
        }
    }
}
