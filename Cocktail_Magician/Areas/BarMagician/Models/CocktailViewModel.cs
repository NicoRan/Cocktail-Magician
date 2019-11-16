using Cocktail_Magician_DB.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cocktail_Magician.Areas.BarMagician.Models
{
    public class CocktailViewModel
    {
        public CocktailViewModel()
        {

        }

        public string CocktailId { get; set; }
        [Required(ErrorMessage = "A name is required!")]
        [MinLength(3, ErrorMessage = "Name should be between 3 and 35 symbols!"),
            MaxLength(35, ErrorMessage = "Name should be between 3 and 35 symbols!")]
        public string Name { get; set; }
        [MinLength(5, ErrorMessage = "Information should be atleast 5 symbols!")]
        public string Information { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public string Picture { get; set; }
        public double Rating { get; set; }
        [Required(ErrorMessage = "At least one ingredient is required!")]
        public List<string> Ingredients { get; set; }
    }
}
