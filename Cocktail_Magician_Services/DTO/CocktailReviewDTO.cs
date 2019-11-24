using System;
using System.ComponentModel.DataAnnotations;

namespace Cocktail_Magician_Services.DTO
{
    public class CocktailReviewDTO
    {
        public string CocktailId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
        public double Grade { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string UserPicture { get; set; }
    }
}
