using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Mappers
{
    public static class CocktailCommentDTOMapper
    {
        public static CocktailComment ToCommentEntity(this CocktailReviewDTO cocktailComment)
        {
            var cocktailCommentDTO = new CocktailComment
            {
                UserId = cocktailComment.UserId,
                CocktailId = cocktailComment.CocktailId,
                Comment = cocktailComment.Comment
            };
            return cocktailCommentDTO;
        }

        //public static ICollection<CocktailCommentDTO> ToDTO(this ICollection<CocktailComment> cocktailComments)
        //{
        //    var newCollection = cocktailComments.Select(c => c.ToDTO()).ToList();
        //    return newCollection;
        //}
    }
}
