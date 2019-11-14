using Cocktail_Magician.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class CreateBarReviewVeiwModelMapper
    {
        public static BarReviewDTO ToBarDTO(this CreateReviewViewModel createReviewViewModel)
        {
            var barCommentDTO = new BarReviewDTO
            {
                BarId = createReviewViewModel.Id,
                UserId = createReviewViewModel.UserId,
                Comment = createReviewViewModel.Comment,
                Grade = createReviewViewModel.Rate
            };
            return barCommentDTO;
        }

        //public static ICollection<BarCommentDTO> ToDTO(this ICollection<CreateReviewViewModel> createReviewViewModels)
        //{
        //    var newCollection = createReviewViewModels.Select(c => c.ToDTO()).ToList();
        //    return newCollection;
        //}

    }
}
