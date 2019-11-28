using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class CocktailReview
    {
        public CocktailReview()
        {

        }
        public CocktailReview(double grade, string comment, string userId, string cocktailId, DateTime createdOn)
        {
            Grade = grade;
            Comment = comment;
            UserId = userId;
            CocktailId = cocktailId;
            CreatedOn = createdOn;
        }
        public string Comment { get; set; }
        public double Grade { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
