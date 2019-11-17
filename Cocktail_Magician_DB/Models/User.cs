using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Cocktail_Magician_DB.Models
{
    public class User : IdentityUser
    {
        public string Picture { get; set; }
        public ICollection<BarReview> BarReviews { get; set; }
        public ICollection<CocktailReview> CocktailReviews { get; set; }
    }
}
