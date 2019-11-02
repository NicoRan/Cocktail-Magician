using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician.Models;
using Cocktail_Magician_Services.Contracts;

namespace Cocktail_Magician.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBarManager _barManager;
        public HomeController(IBarManager barManager)
        {
            this._barManager = barManager;
        }
        public async Task<IActionResult> Index()
        {
            var topBars = await this._barManager.GetTopRatedBars();
            var topBarsViewModel = topBars.Select(bar => new FrontPageTopRatedBarsViewModel
            {
                Name = bar.Name,
                Address = bar.Address,
                Rating = (double)bar.Rating,
                Picture = bar.Picture
            })
                .ToList();
            var model = new AllClassModels();
            model.Index = topBarsViewModel;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
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
