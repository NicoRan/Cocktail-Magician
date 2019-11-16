using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_Magician_Services.Mappers
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
        public static BarRating ToRatingEntity(this BarReviewDTO barRatingDTO)
        {
            var barRating = new BarRating
            {
                UserId = barRatingDTO.UserId,
                BarId = barRatingDTO.BarId,
                Grade = barRatingDTO.Grade
            };
            return barRating;
        }
        public static ICollection<BarRatingDTO> ToDTO(this ICollection<BarRating> barRatings)
        {
            var newCollection = barRatings.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
