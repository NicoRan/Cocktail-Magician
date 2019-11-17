using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocktail_Magician_Services.Mappers
{
    public static class BarReviewDTOMapper
    {
        public static BarReviewDTO ToDTO(this BarReview barReview)
        {
            var barReviewDTO = new BarReviewDTO
            {
                UserId = barReview.UserId,
                BarId = barReview.BarId,
                Comment = barReview.Comment,
                Grade = barReview.Grade
            };
            return barReviewDTO;
        }

        public static BarReview ToEntity(this BarReviewDTO barReviewDTO)
        {
            var barReview = new BarReview
            {
                UserId = barReviewDTO.UserId,
                BarId = barReviewDTO.BarId,
                Grade = barReviewDTO.Grade,
                Comment = barReviewDTO.Comment
            };
            return barReview;
        }

        public static ICollection<BarReviewDTO> ToDTO(this ICollection<BarReview> barReviews)
        {
            var newCollection = barReviews.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
