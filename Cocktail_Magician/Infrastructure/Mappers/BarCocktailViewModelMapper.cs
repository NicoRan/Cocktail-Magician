using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class BarCocktailViewModelMapper
    {
        public static BarCocktailDTO ToDTO(this BarCocktailViewModel barCocktailView)
        {
            var barCocktailDTO = new BarCocktailDTO
            {
                BarId = barCocktailView.BarId,
                CocktailId = barCocktailView.CocktailId
            };
            return barCocktailDTO;
        }

        public static BarCocktailViewModel ToVM(this BarCocktailDTO barCocktail)
        {
            var barCocktailDTO = new BarCocktailViewModel
            {
                BarId = barCocktail.BarId,
                CocktailId = barCocktail.CocktailId,
                CocktailName = barCocktail.CocktailName
            };
            return barCocktailDTO;
        }

        public static ICollection<BarCocktailDTO> ToDTO(this ICollection<BarCocktailViewModel> barCocktails)
        {
            var newCollection = barCocktails.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }

        public static ICollection<BarCocktailViewModel> ToVM(this ICollection<BarCocktailDTO> barCocktails)
        {
            var newCollection = barCocktails.Select(c => c.ToVM()).ToList();
            return newCollection;
        }
    }
}
