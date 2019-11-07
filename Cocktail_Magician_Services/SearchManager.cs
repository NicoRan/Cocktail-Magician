using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktail_Magician_Services
{
    public class SearchManager : ISearchManager
    {
        private readonly CMContext _context;
        private readonly IBarManager _barManager;
        private readonly ICocktailManager _cocktailManager;
        public SearchManager(CMContext context, IBarManager barManager, ICocktailManager cocktailManager)
        {
            _context = context;
            _barManager = barManager;
            _cocktailManager = cocktailManager;
        }

        public List<Bar> SearchBars(string criteria)
        {
            var listOfBars = _context.Bars;
            var result = listOfBars.Where(bar => bar.Name.Contains(criteria)).ToList();
            return result.OrderBy(r => r.Name).ToList();
        }

        public List<Cocktail> SearchCocktails(string criteria)
        {
            var listOfCocktails = _context.Cocktails;
            var result = listOfCocktails.Where(cocktail => cocktail.Name.Contains(criteria)).ToList();
            return result.OrderBy(r => r.Name).ToList();
        }
    }
}
