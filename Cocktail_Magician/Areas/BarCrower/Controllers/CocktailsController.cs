using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician.Areas.BarCrower.Models;
using System.Security.Claims;
using Cocktail_Magician.Infrastructure.Mappers;
using Cocktail_Magician_Services.DTO;
using Cocktail_Magician.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cocktail_Magician.Areas.BarCrower.Controllers
{
    [Area("BarCrower")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailManager _cocktailManager;

        public CocktailsController(ICocktailManager cocktailManager)
        {
            _cocktailManager = cocktailManager;
        }

        // GET: BarCrower/Cocktails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid review parameters!");
            }
            try
            {
                var cocktail = await _cocktailManager.GetCocktail(id);
                var cocktailViewModel = CocktailViewModelMapper.MapCocktailViewModel(cocktail);
                return View(cocktailViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel 
                { 
                    ErrorCode = "404",
                    ErrorMessage = ex.Message
                });
            }
        }

        [Authorize(Roles = "Member")]
        public IActionResult Review()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Review(CreateReviewViewModel reviewVeiwModel)
        {
            
            var cocktailReview = reviewVeiwModel.ToCocktailDTO();

            if (ModelState.IsValid)
            {
                await _cocktailManager.CreateCocktailReviewAsync(cocktailReview);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
