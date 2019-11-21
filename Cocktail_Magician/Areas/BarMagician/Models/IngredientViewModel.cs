using Cocktail_Magician_DB.Models;
using System.ComponentModel.DataAnnotations;

namespace Cocktail_Magician.Areas.BarMagician.Models
{
    public class IngredientViewModel
    {

        public string Id { get; set; }
        [MinLength(3, ErrorMessage = "Ingredient's name should be between 3 and 15 characters!"),
            MaxLength(15, ErrorMessage = "Ingredient's name should be between 3 and 15 characters!")]
        public string Name { get; set; }
    }
}
