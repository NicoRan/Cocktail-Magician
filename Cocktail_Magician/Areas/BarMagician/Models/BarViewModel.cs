using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cocktail_Magician.Areas.BarMagician.Models
{
    public class BarViewModel
    {
        public string BarId { get; set; }
        [Required(ErrorMessage = "A name is required!")]
        [MinLength(3, ErrorMessage = "Name should be between 3 and 35 symbols!"),
            MaxLength(35, ErrorMessage = "Name should be between 3 and 35 symbols!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "An address is required!")]
        [MinLength(5, ErrorMessage = "Address should at least 5 symbols!")]
        public string Address { get; set; }
        public double Rating { get; set; }
        [MinLength(5, ErrorMessage = "Information should be atleast 5 symbols!")]
        public string Information { get; set; }
        public string Picture { get; set; }
        public string Map { get; set; }
        public bool IsRated { get; set; }
        public ICollection<ReviewViewModel> ReviewViewModels { get; set; }
        public ICollection<BarCocktailViewModel> BarCocktailViewModels { get; set; }
    }
}
