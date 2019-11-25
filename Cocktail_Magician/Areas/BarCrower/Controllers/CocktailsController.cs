using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Infrastructure.Mappers;
using Cocktail_Magician.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
                ModelState.AddModelError(string.Empty, "Invalid cocktail parameters!");
            }
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cocktail = await _cocktailManager.GetCocktail(id);
                var cocktailViewModel = cocktail.ToVM();
                cocktailViewModel.ReviewViewModels = (await _cocktailManager.GetAllReviewsByCocktailID(id)).ToCocktailReviewVM();
                cocktailViewModel.IsRated = await _cocktailManager.IsReviewGiven(id, userId);
                return View(cocktailViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
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
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid cocktail parameters!");
                return View();
            }
            try
            {
                var cocktailReview = reviewVeiwModel.ToCocktailDTO();
                await _cocktailManager.CreateCocktailReviewAsync(cocktailReview);
                return RedirectToAction("Details", "Cocktails", new { id = cocktailReview.CocktailId });
            }
            catch(Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
            }
        }
    }
}
