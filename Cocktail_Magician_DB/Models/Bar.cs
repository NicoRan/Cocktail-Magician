using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class Bar
    {
        public Bar()
        {

        }
        public Bar(string name, string address, double rating, string picture)
        {
            this.Name = name;
            this.Address = address;
            this.Rating = rating;
            this.Picture = picture;
        }
        public string BarId { get; set; }
        [MinLength(3, ErrorMessage = "Name should be between 3 and 35 symbols!")]
        [MaxLength(35, ErrorMessage = "Name should be between 3 and 35 symbols!")]
        public string Name { get; set; }
        [MinLength(5, ErrorMessage = "Address should at least 5 symbols!")]
        public string Address { get; set; }
        public double Rating { get; set; }
        public string Picture { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<BarRating> BarRatings { get; set; }
        public ICollection<BarComment> BarComments { get; set; }
    }
}
