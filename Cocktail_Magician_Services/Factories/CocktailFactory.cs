using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_Services.Factories
{
    public class CocktailFactory : ICocktailFactory
    {
        public Cocktail CreateNewCocktail(string name, string picture)
        {
            if (name.Length < 3 || name.Length > 35)
                throw new Exception("Name should be between 3 and 35 symbols!");
            return new Cocktail(name, picture);
        }

        public CocktailReview CreateNewCocktailReview(double grade, string comment, string userId, string cocktailId, DateTime createdOn)
        {
            if (comment.Length > 500)
                throw new Exception("Comment should be not more than 500 characters!");
            if (grade < 0 || grade > 5)
                throw new Exception("Grade should be between 0 and 5!");
            return new CocktailReview(grade, comment, userId, cocktailId, createdOn);
        }
    }
}
