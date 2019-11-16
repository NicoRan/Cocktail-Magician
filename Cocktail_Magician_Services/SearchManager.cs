using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician_Services
{
    public class SearchManager : ISearchManager
    {
        private readonly IBarManager _barManager;
        private readonly ICocktailManager _cocktailManager;
        public SearchManager(IBarManager barManager, ICocktailManager cocktailManager)
        {
            _barManager = barManager;
            _cocktailManager = cocktailManager;
        }

        public async Task<List<Bar>> SearchBarsByName(string criteria)
        {
            var listOfBars = await _barManager.GetAllBarsAsync();
            return listOfBars.Where(bar => bar.Name.Contains(criteria, StringComparison.CurrentCultureIgnoreCase)).OrderBy(r => r.Name).ToList();
        }
        public async Task<List<Bar>> SearchBarsByAddress(string criteria)
        {
            var address = await _barManager.GetAllBarsAsync();
            return address.Where(bar => bar.Address.Contains(criteria, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(r => r.Name).ToList(); ;
        }
        public async Task<List<Cocktail>> SearchCocktails(string criteria)
        {
            var listOfCocktails = await _cocktailManager.GetAllCocktailsAsync();
            return listOfCocktails.Where(cocktail => cocktail.Name.Contains(criteria, StringComparison.CurrentCultureIgnoreCase)).OrderBy(r => r.Name).ToList();
        }
    }
}
