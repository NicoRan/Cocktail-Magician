using System.Linq;
using System.Threading.Tasks;
using Cocktail_Magician.Infrastructure.Mappers;
using Cocktail_Magician.Models;
using Cocktail_Magician_Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cocktail_Magician.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchManager _searchManager;
        public SearchController(ISearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchResultViewModel model)
        {
            var results = new SearchResultViewModel();
            var filteredResults = new SearchResultViewModel();
            int stars = 0;
            if (model.StarsRange != null)
            {
                stars = model.StarsRange.Count(x => x == '/');
            }
            
            if (model.Filter == null)
            {
                results = await SearchByTypeAndCriteria(model);
                return View(results);
            }
            else if(model.Filter["name"] == true || model.Filter["address"] == true)
            {
                if(model.Filter["name"] == true)
                {
                    results = await SearchByTypeAndCriteria(model);
                    filteredResults.Bars = results.Bars;
                    filteredResults.Cocktails = results.Cocktails;
                }
                if(model.Filter["address"] == true)
                {
                    results = await SearchByTypeAndCriteriaAddress(model);
                    filteredResults.Bars = results.Bars;
                    filteredResults.Cocktails = results.Cocktails;
                }
            }
            else
            {
                filteredResults = await SearchByTypeAndCriteria(model);
            }
            if(stars > 1)
            {
                filteredResults.Bars = filteredResults.Bars != null ? filteredResults.Bars.Where(b => b.Rating >= stars).ToList() : null;
                filteredResults.Cocktails = filteredResults.Cocktails != null ? filteredResults.Cocktails.Where(b => b.Rating >= stars).ToList() : null;
            }
            if(model.SortOptions != null)
            {
                switch (model.SortOptions)
                {
                    case "sort-by-1":
                        filteredResults.Bars = filteredResults.Bars != null ? filteredResults.Bars.OrderBy(b => b.Name).ToList() : null;
                        filteredResults.Cocktails = filteredResults.Cocktails != null ? filteredResults.Cocktails.OrderBy(b => b.Name).ToList() : null;
                        break;
                    case "sort-by-2":
                        filteredResults.Bars = filteredResults.Bars != null ? filteredResults.Bars.OrderByDescending(b => b.Name).ToList() : null;
                        filteredResults.Cocktails = filteredResults.Cocktails != null ? filteredResults.Cocktails.OrderByDescending(b => b.Name).ToList() : null;
                        break;
                    case "sort-by-3":
                        filteredResults.Bars = filteredResults.Bars != null ? filteredResults.Bars.OrderByDescending(b => b.Rating).ToList() : null;
                        filteredResults.Cocktails = filteredResults.Cocktails != null ? filteredResults.Cocktails.OrderByDescending(b => b.Rating).ToList() : null;
                        break;
                    case "sort-by-4":
                        filteredResults.Bars = filteredResults.Bars != null ? filteredResults.Bars.OrderBy(b => b.Rating).ToList() : null;
                        filteredResults.Cocktails = filteredResults.Cocktails != null ? filteredResults.Cocktails.OrderBy(b => b.Rating).ToList() : null;
                        break;
                }
            }
            return View(filteredResults);
        }

        private async Task<SearchResultViewModel> SearchByTypeAndCriteria(SearchResultViewModel model)
        {
            var resultsView = new SearchResultViewModel();
            if (model.Type == "Bars")
            {
                var resultsBars = await _searchManager.SearchBarsByName(model.Criteria);
                resultsView.Bars = resultsBars.Select(bar => bar.ToVM()).ToList();
                return resultsView;
            }
            else if (model.Type == "Cocktails")
            {
                var resultsCocktails = await _searchManager.SearchCocktails(model.Criteria);
                resultsView.Cocktails = resultsCocktails.Select(cocktail => cocktail.ToVM()).ToList();
                return resultsView;
            }
            else if (model.Type == "All")
            {
                var resultsBars = await _searchManager.SearchBarsByName(model.Criteria);
                resultsView.Bars = resultsBars.Select(bar => bar.ToVM()).ToList();

                var resultsCocktails = await _searchManager.SearchCocktails(model.Criteria);
                resultsView.Cocktails = resultsCocktails.Select(cocktail => cocktail.ToVM()).ToList();

                return resultsView;
            }
            return resultsView;
        }

        private async Task<SearchResultViewModel> SearchByTypeAndCriteriaAddress(SearchResultViewModel model)
        {
            var resultsView = new SearchResultViewModel();
            if (model.Type == "Bars")
            {
                var resultsBars = await _searchManager.SearchBarsByAddress(model.Criteria);
                resultsView.Bars = resultsBars.Select(bar => bar.ToVM()).ToList();
                return resultsView;
            }
            else if (model.Type == "Cocktails")
            {
                var resultsCocktails = await _searchManager.SearchCocktails(model.Criteria);
                resultsView.Cocktails = resultsCocktails.Select(cocktail => cocktail.ToVM()).ToList();
                return resultsView;
            }
            else if (model.Type == "All")
            {
                var resultsBars = await _searchManager.SearchBarsByAddress(model.Criteria);
                resultsView.Bars = resultsBars.Select(bar => bar.ToVM()).ToList();

                var resultsCocktails = await _searchManager.SearchCocktails(model.Criteria);
                resultsView.Cocktails = resultsCocktails.Select(cocktail => cocktail.ToVM()).ToList();

                return resultsView;
            }
            return resultsView;
        }
    }
}