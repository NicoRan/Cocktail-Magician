using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cocktail_Magician_DB.Models
{
    public class Bar
    {
        public Bar()
        {

        }
        public Bar(string name, string address, string information, string picture, string mapDirections)
        {
            Name = name;
            Address = address;
            Information = information;
            Picture = picture;
            MapDirections = mapDirections;
        }
        public string BarId { get; set; }
        [Required(ErrorMessage = "A name is required!")]
        [MinLength(3, ErrorMessage = "Name should be between 3 and 35 symbols!"),
            MaxLength(35, ErrorMessage = "Name should be between 3 and 35 symbols!")]
        public string Name { get; set; }
        [MinLength(5, ErrorMessage = "Address should be at least 5 symbols!")]
        public string Address { get; set; }
        [MinLength(5, ErrorMessage = "Information should be atleast 5 symbols!")]
        public string Information { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public double Rating { get; set; }
        public string Picture { get; set; }
        public string MapDirections { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<BarReview> BarReviews { get; set; }
    }
}
