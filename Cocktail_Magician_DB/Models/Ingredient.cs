using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cocktail_Magician_DB.Models
{
    public class Ingredient
    {
        public Ingredient()
        {

        }
        public Ingredient(string name)
        {
            this.Name = name;
        }
        public string IngredientId { get; set; }
        [Required(ErrorMessage = "A name is required!")]
        [MinLength(3, ErrorMessage = "Name should be between 3 and 35 symbols!"),
            MaxLength(35, ErrorMessage = "Name should be between 3 and 35 symbols!")]
        public string Name { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredient { get; set; }
    }
}
