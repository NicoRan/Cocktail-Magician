﻿using System;
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

namespace Cocktail_Magician.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBarManager _barManager;
        private readonly ICocktailManager _cocktailManager;
        private readonly UserManager<User> _userManager;

        public HomeController(ICocktailManager cocktailManager, IBarManager barManager, UserManager<User> userManager)
        {
            _cocktailManager = cocktailManager;
            _barManager = barManager;
            _userManager = userManager;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
