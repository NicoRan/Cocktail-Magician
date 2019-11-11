using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static ICollection<BarCommentDTO> ToDTO(this ICollection<BarComment> barComments)
        {
            var newCollection = barComments.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
