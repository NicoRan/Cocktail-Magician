using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_Services.DTO
{
    public class CocktailReviewDTO
    {
        public string CocktailId { get; set; }
        public string UserId { get; set; }
        public double Grade { get; set; }
        public string Comment { get; set; }
    }
}
