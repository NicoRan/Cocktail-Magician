using System.Collections.Generic;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ISearchManager
    {
        List<Bar> SearchBarsByName(string criteria);
        List<Bar> SearchBarsByAddress(string criteria);
        List<Cocktail> SearchCocktails(string criteria);
    }
}