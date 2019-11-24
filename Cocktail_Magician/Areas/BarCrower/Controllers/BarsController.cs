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
    public class BarsController : Controller
    {
        private readonly IBarManager _barManager;

        public BarsController(IBarManager barManager)
        {
            _barManager = barManager;
        }

        // GET: BarCrower/Bars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid bar parameters!");
            }
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var bar = await _barManager.GetBarForDetails(id);
                var barViewModel = bar.ToVM();
                barViewModel.IsRated = await _barManager.IsReviewGiven(id, user);
                return View(barViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message});
            }
        }


        [HttpGet]
        [Authorize(Roles = "Member")]
        public IActionResult Review(string id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Review(CreateReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var barReview = reviewViewModel.ToBarDTO();
                    await _barManager.CreateBarReviewAsync(barReview);
                    return RedirectToAction("Index", "Home");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
                }
            }
            return View();
        }
    }
}
