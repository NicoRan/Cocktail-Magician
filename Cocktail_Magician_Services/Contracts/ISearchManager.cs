using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ISearchManager
    {
        Task<List<Bar>> SearchBarsByName(string criteria);
        Task<List<Bar>> SearchBarsByAddress(string criteria);
        Task<List<Cocktail>> SearchCocktails(string criteria);
    }
}