using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician.Models;
using Cocktail_Magician_DB.Models;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(SearchResultViewModel model)
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
                results = SearchByTypeAndCriteria(model);
                return View(results);
            }
            else if(model.Filter["name"] == true || model.Filter["address"] == true)
            {
                if(model.Filter["name"] == true)
                {
                    results = SearchByTypeAndCriteria(model);
                    filteredResults.Bars = results.Bars;
                    filteredResults.Cocktails = results.Cocktails;
                }
                if(model.Filter["address"] == true)
                {
                    results = SearchByTypeAndCriteriaA(model);
                    filteredResults.Bars = results.Bars;
                    filteredResults.Cocktails = results.Cocktails;
                }
            }
            else
            {
                filteredResults = SearchByTypeAndCriteria(model);
            }
            if(stars > 1)
            {
                filteredResults.Bars = filteredResults.Bars.Where(b => b.Rating >= stars).ToList() ??null;
                filteredResults.Cocktails = filteredResults.Cocktails != null ? filteredResults.Cocktails.Where(c => c.Rating >= stars).ToList() : null;
            }
            return View(filteredResults);
        }

        private SearchResultViewModel SearchByTypeAndCriteria(SearchResultViewModel model)
        {
            //After Implementing Mappers, this will be simplified
            var resultsBars = new List<Bar>();
            var resultsCocktails = new List<Cocktail>();
            var resultsView = new SearchResultViewModel();
            if (model.Type == "Bars")
            {
                resultsBars = _searchManager.SearchBarsByName(model.Criteria);
                resultsView.Bars = resultsBars.Select(r => new BarViewModel
                {
                    BarId = r.BarId,
                    Name = r.Name,
                    Address = r.Address,
                    Information = r.Information,
                    Picture = r.Picture,
                    Rating = r.Rating,
                    Map = r.MapDirections
                }).ToList();
                return resultsView;
            }
            else if (model.Type == "Cocktails")
            {
                resultsCocktails = _searchManager.SearchCocktails(model.Criteria);
                resultsView.Cocktails = resultsCocktails.Select(r => new CocktailViewModel
                {
                    CocktailId = r.Id,
                    Name = r.Name,
                    Picture = r.Picture,
                    Information = r.Information,
                    Rating = r.Rating
                }).ToList();
                return resultsView;
            }
            else if (model.Type == "All")
            {
                resultsBars = _searchManager.SearchBarsByName(model.Criteria);
                resultsView.Bars = resultsBars.Select(r => new BarViewModel
                {
                    BarId = r.BarId,
                    Name = r.Name,
                    Address = r.Address,
                    Information = r.Information,
                    Picture = r.Picture,
                    Rating = r.Rating,
                    Map = r.MapDirections
                }).ToList();

                resultsCocktails = _searchManager.SearchCocktails(model.Criteria);
                resultsView.Cocktails = resultsCocktails.Select(r => new CocktailViewModel
                {
                    CocktailId = r.Id,
                    Name = r.Name,
                    Picture = r.Picture,
                    Information = r.Information,
                    Rating = r.Rating
                }).ToList();

                return resultsView;
            }
            return resultsView;
        }

        private SearchResultViewModel SearchByTypeAndCriteriaA(SearchResultViewModel model)
        {
            //After Implementing Mappers, this will be simplified
            var resultsBars = new List<Bar>();
            var resultsCocktails = new List<Cocktail>();
            var resultsView = new SearchResultViewModel();
            if (model.Type == "Bars")
            {
                resultsBars = _searchManager.SearchBarsByAddress(model.Criteria);
                resultsView.Bars = resultsBars.Select(r => new BarViewModel
                {
                    BarId = r.BarId,
                    Name = r.Name,
                    Address = r.Address,
                    Information = r.Information,
                    Picture = r.Picture,
                    Rating = r.Rating,
                    Map = r.MapDirections
                }).ToList();
                return resultsView;
            }
            else if (model.Type == "Cocktails")
            {
                resultsCocktails = _searchManager.SearchCocktails(model.Criteria);
                resultsView.Cocktails = resultsCocktails.Select(r => new CocktailViewModel
                {
                    CocktailId = r.Id,
                    Name = r.Name,
                    Picture = r.Picture,
                    Information = r.Information,
                    Rating = r.Rating
                }).ToList();
                return resultsView;
            }
            else if (model.Type == "All")
            {
                resultsBars = _searchManager.SearchBarsByAddress(model.Criteria);
                resultsView.Bars = resultsBars.Select(r => new BarViewModel
                {
                    BarId = r.BarId,
                    Name = r.Name,
                    Address = r.Address,
                    Information = r.Information,
                    Picture = r.Picture,
                    Rating = r.Rating,
                    Map = r.MapDirections
                }).ToList();

                resultsCocktails = _searchManager.SearchCocktails(model.Criteria);
                resultsView.Cocktails = resultsCocktails.Select(r => new CocktailViewModel
                {
                    CocktailId = r.Id,
                    Name = r.Name,
                    Picture = r.Picture,
                    Information = r.Information,
                    Rating = r.Rating
                }).ToList();

                return resultsView;
            }
            return resultsView;
        }
    }
}