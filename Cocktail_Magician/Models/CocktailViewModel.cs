using Cocktail_Magician_DB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class CocktailViewModel
    {
        public CocktailViewModel()
        {

        }
        public CocktailViewModel(Cocktail cocktail)
        {
            CocktailId = cocktail.Id;
            Name = cocktail.Name;
            Ingredients = new List<string>();
        }

        public string CocktailId { get; set; }
        [Required(ErrorMessage = "A name is required!")]
        [MinLength(3, ErrorMessage = "Name should be between 3 and 35 symbols!"),
            MaxLength(35, ErrorMessage = "Name should be between 3 and 35 symbols!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "At least one ingredient is required!")]
        public List<string> Ingredients { get; set; }
    }
}
