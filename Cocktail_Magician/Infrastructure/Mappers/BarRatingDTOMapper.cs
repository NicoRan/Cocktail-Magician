using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class BarRatingDTOMapper
    {
        public static BarRatingDTO ToDTO(this BarRating barRating)
        {
            var barRatingDTO = new BarRatingDTO
            {
                UserId = barRating.UserId,
                BarId = barRating.BarId,
                Grade = barRating.Grade
            };
            return barRatingDTO;
        }

        public static ICollection<BarRatingDTO> ToDTO(this ICollection<BarRating> barRatings)
        {
            var newCollection = barRatings.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
