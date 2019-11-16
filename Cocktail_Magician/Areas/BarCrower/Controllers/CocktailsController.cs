using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Infrastructure.Mappers;
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
                ModelState.AddModelError(string.Empty, "Invalid cocktail parameters!");
            }
            try
            {
                var cocktail = await _cocktailManager.GetCocktail(id);
                var cocktailViewModel = CocktailViewModelMapper.MapCocktailViewModel(cocktail);
                return View(cocktailViewModel);
            }
            catch (Exception ex)
            {
                return RedirecToActionError("404", ex.Message);
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

        private IActionResult RedirecToActionError(string errorCode, string errorMessage)
        {
            return RedirectToAction("Error", "Home", new ErrorViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            });
        }
    }
}
