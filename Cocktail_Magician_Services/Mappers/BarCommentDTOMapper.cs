using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_Magician_Services.Mappers
{
    public static class BarCommentDTOMapper
    {
        public static BarCommentDTO ToDTO(this BarComment barComment)
        {
            var barCommentDTO = new BarCommentDTO
            {
                BarId = barComment.BarId,
                UserId = barComment.UserId,
                Comment = barComment.Comment
            };
            return barCommentDTO;
        }
        public static BarComment ToCommentEntity(this BarReviewDTO barCommentDTO)
        {
            var barComment = new BarComment
            {
                UserId = barCommentDTO.UserId,
                BarId = barCommentDTO.BarId,
                Comment = barCommentDTO.Comment
            };
            return barComment;
        }
        public static ICollection<BarCommentDTO> ToDTO(this ICollection<BarComment> barComments)
        {
            var newCollection = barComments.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
