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
                Rating = bar.BarReviews.Any(br => br.BarId == bar.BarId) ? bar.BarReviews.Average(br => br.Grade) : 0,
            };
            barDTO.BarReviewDTOs = bar.BarReviews.Select(b => b.ToDTO()).ToList();
            barDTO.CocktailDTOs = bar.Cocktails.ToDTO();
            return barDTO;
        }

        public static ICollection<BarDTO> ToDTO(this ICollection<Bar> bars)
        {
            var newCollection = bars.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
