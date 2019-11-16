using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Cocktail_Magician_DB.Models
{
    public class User : IdentityUser
    {
        public string Picture { get; set; }
        public ICollection<BarRating> BarRatings { get; set; }
        public ICollection<BarComment> BarComments { get; set; }
        public ICollection<CocktailComment> CocktailComments { get; set; }
        public ICollection<CocktailRating> CocktailRatings { get; set; }
    }
}
