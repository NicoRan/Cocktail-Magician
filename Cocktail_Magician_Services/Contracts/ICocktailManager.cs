using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ICocktailManager
    {
        Task<Cocktail> CreateCocktail(Cocktail cocktail, List<Ingredient> ingredients);
    }
}