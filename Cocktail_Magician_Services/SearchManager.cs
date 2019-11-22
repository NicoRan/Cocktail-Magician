using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
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

        public async Task<ICollection<BarDTO>> SearchBarsByName(string criteria)
        {
            var listOfBars = await _barManager.GetAllBarsAsync();
            if (string.IsNullOrEmpty(criteria) || string.IsNullOrWhiteSpace(criteria))
            {
                return listOfBars;
            }
            return listOfBars.Where(bar => bar.Name.Contains(criteria, StringComparison.CurrentCultureIgnoreCase)).OrderBy(r => r.Name).ToList();
        }
        public async Task<ICollection<BarDTO>> SearchBarsByAddress(string criteria)
        {
            var address = await _barManager.GetAllBarsAsync();
            if (string.IsNullOrEmpty(criteria) || string.IsNullOrWhiteSpace(criteria))
            {
                return address;
            }
            return address.Where(bar => bar.Address.Contains(criteria, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(r => r.Name).ToList(); ;
        }
        public async Task<ICollection<CocktailDTO>> SearchCocktails(string criteria)
        {
            var listOfCocktails = await _cocktailManager.GetAllCocktailsAsync();
            if (string.IsNullOrEmpty(criteria) || string.IsNullOrWhiteSpace(criteria))
            {
                return listOfCocktails;
            }
            return listOfCocktails.Where(cocktail => cocktail.Name.Contains(criteria, StringComparison.CurrentCultureIgnoreCase)).OrderBy(r => r.Name).ToList();
        }
    }
}
