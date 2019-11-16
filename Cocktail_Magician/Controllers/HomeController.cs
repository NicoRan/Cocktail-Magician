using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician.Infrastructure.Mappers;

namespace Cocktail_Magician.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBarManager _barManager;
        private readonly ICocktailManager _cocktailManager;

        public HomeController(ICocktailManager cocktailManager, IBarManager barManager)
        {
            _cocktailManager = cocktailManager;
            _barManager = barManager;
        }
        public async Task<IActionResult> Index()
        {
            var topBars = await _barManager.GetTopRatedBars();
            var topBarsViewModel = topBars.Select(bar => new BarViewModel(bar))
                .ToList();
            var topCocktails = await _cocktailManager.GetTopRatedCocktails();
            var topCocktailsViewModel = topCocktails.Select(cocktail => new CocktailViewModel(cocktail)).ToList();
            var topRatedHomePage = new TopRatedHomePageViewModel();
            topRatedHomePage.TopBars = topBarsViewModel;
            topRatedHomePage.TopCocktails = topCocktailsViewModel;
            return View(topRatedHomePage);
        }

        public async Task<IActionResult> BarCatalog()
        {
            var listOfBars = await _barManager.GetAllBarsAsync();
            var listOfBarsViewModel = new List<BarViewModel>();
            foreach (var bar in listOfBars)
            {
                var mapToView = BarViewModelMapper.MapBarViewModel(bar);
                listOfBarsViewModel.Add(mapToView);
            }
            return View(listOfBarsViewModel);
        }

        public async Task<IActionResult> CocktailCatalog()
        {
            var listOfCocktails = await _cocktailManager.GetAllCocktailsAsync();
            var listOfCocktailsViewModel = new List<CocktailViewModel>();
            foreach (var cocktail in listOfCocktails)
            {
                var mapToView = CocktailViewModelMapper.MapCocktailViewModel(cocktail);
                listOfCocktailsViewModel.Add(mapToView);
            }
            return View(listOfCocktailsViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel error)
        {
            return View(error);
        }
    }
}
