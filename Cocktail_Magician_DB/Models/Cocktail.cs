using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class Cocktail
    {
        public Cocktail()
        {

        }
        public Cocktail(string name)
        {
            this.Name = name;
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "A name is required!")]
        [MinLength(3, ErrorMessage = "Name should be between 3 and 35 symbols!"),
            MaxLength(35, ErrorMessage = "Name should be between 3 and 35 symbols!")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredient { get; set; }
        public ICollection<CocktailComment> CocktailComments { get; set; }
        public ICollection<CocktailRating> CocktailRatings { get; set; }
    }
}
