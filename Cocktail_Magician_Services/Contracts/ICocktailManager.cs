using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ICocktailManager
    {
        Task<Cocktail> CreateCocktail(Cocktail cocktail, List<string> ingredient);
        Task<List<Cocktail>> GetTopRatedCocktails();
        Task<Cocktail> GetCocktail(string id);
        Task RemoveCocktail(string id);
    }
}