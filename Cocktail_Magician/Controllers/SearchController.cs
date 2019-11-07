using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Search([FromQuery] string criteria, [FromQuery] string type)
        {
            //After Implementing Mappers, this will be simplified
            var resultsBars = new List<Bar>();
            var resultsCocktails = new List<Cocktail>();
            var resultsView = new SearchResultViewModel();
            if (type == "Bars")
            {
                resultsBars = _searchManager.SearchBars(criteria);
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
                return View(resultsView);
            }
            else if (type == "Cocktails")
            {
                resultsCocktails = _searchManager.SearchCocktails(criteria);
                resultsView.Cocktails = resultsCocktails.Select(r => new CocktailViewModel
                {
                    CocktailId = r.Id,
                    Name = r.Name,
                    Picture = r.Picture,
                    Information = r.Information,
                    Rating = r.Rating
                }).ToList();
                return View(resultsView);
            }
            else if(type == "All")
            {
                resultsBars = _searchManager.SearchBars(criteria);
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

                resultsCocktails = _searchManager.SearchCocktails(criteria);
                resultsView.Cocktails = resultsCocktails.Select(r => new CocktailViewModel
                {
                    CocktailId = r.Id,
                    Name = r.Name,
                    Picture = r.Picture,
                    Information = r.Information,
                    Rating = r.Rating
                }).ToList();

                return View(resultsView);
            }
            return View();
        }
    }
}