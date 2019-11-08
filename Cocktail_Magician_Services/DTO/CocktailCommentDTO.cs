using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_Services.DTO
{
    public class CocktailCommentDTO
    {
        public string UserId { get; set; }
        public string CocktailId { get; set; }
        public string Comment { get; set; }
    }   
}
