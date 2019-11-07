using System.Collections.Generic;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ISearchManager
    {
        List<Bar> SearchBars(string criteria);
        List<Cocktail> SearchCocktails(string criteria);
    }
}