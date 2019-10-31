using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class User : IdentityUser
    {
        public ICollection<BarRating> BarRatings { get; set; }
        public ICollection<BarComment> BarComments { get; set; }
        public ICollection<CocktailComment> CocktailComments { get; set; }
        public ICollection<CocktailRating> CocktailRatings { get; set; }
    }
}
