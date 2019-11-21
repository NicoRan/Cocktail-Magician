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
                Rating = bar.BarReviews.Any(br => br.BarId == bar.BarId) ? bar.BarReviews.Average(br => br.Grade) : 0
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
                Rating = barDTO.BarReviewDTOs.Any(br => br.BarId == barDTO.Id) ? barDTO.BarReviewDTOs.Average(br => br.Grade) : 0
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
