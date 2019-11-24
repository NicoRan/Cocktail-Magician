using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Areas.BarMagician.Models
{
    public class CreateBarViewModel
    {
        public CreateBarViewModel()
        {
            Bar = new BarViewModel();
            CocktailsThatCanOffer = new List<CocktailViewModel>();
        }
        public BarViewModel Bar { get; set; }
        public List<CocktailViewModel> CocktailsThatCanOffer { get; set; }
        public List<string> CocktailsToOffer { get; set; }
        public List<string> CocktailsToRemove { get; set; }
    }
}
