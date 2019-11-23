using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_Magician_Services.Mappers
{
    public static class BarDTOMapper
    {
        public static BarDTO ToDTO(this Bar bar)
        {
            var barDTO = new BarDTO
            {
                Id = bar.BarId,
                Name = bar.Name,
                Information = bar.Information,
                Address = bar.Address,
                Picture = bar.Picture,
                MapDirection = bar.MapDirections,
                IsDeleted = bar.IsDeleted,
                Rating = bar.Rating
            };
            barDTO.BarReviewDTOs = bar.BarReviews.Select(b => b.ToDTO()).ToList();
            barDTO.BarCocktailDTOs = bar.BarCocktails.Select(b => b.ToDTO()).ToList();
            return barDTO;
        }

        public static Bar ToBar(this BarDTO barDTO)
        {
            var bar = new Bar
            {
                BarId = barDTO.Id,
                Address = barDTO.Address,
                Information = barDTO.Information,
                MapDirections = barDTO.MapDirection,
                IsDeleted = barDTO.IsDeleted,
                Name = barDTO.Name,
                Picture = barDTO.Picture,
                Rating = barDTO.Rating
            };
            bar.BarReviews = barDTO.BarReviewDTOs.Select(b => b.ToEntity()).ToList();
            bar.BarCocktails = barDTO.BarCocktailDTOs.Select(b => b.ToEntity()).ToList();
            return bar;
        }

        public static ICollection<BarDTO> ToDTO(this ICollection<Bar> bars)
        {
            var newCollection = bars.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
