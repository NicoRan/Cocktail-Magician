using System.Collections.Generic;

namespace Cocktail_Magician.Areas.BarMagician.Models
{
    public class EditCocktailViewModel
    {
        public EditCocktailViewModel()
        {
            Cocktail = new CocktailViewModel();
            IngreddientsThatCanAdd = new List<IngredientViewModel>();
        }
        public CocktailViewModel Cocktail { get; set; }
        public List<IngredientViewModel> IngreddientsThatCanAdd { get; set; }
        public List<string> IngredientsToRemove { get; set; }
        public List<string> IngredientsToAdd { get; set; }
    }
}
