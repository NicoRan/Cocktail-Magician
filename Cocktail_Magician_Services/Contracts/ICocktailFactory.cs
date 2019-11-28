using System;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ICocktailFactory
    {
        Cocktail CreateNewCocktail(string name, string picture);
        CocktailReview CreateNewCocktailReview(double grade, string comment, string userId, string cocktailId, DateTime createdOn);
    }
}