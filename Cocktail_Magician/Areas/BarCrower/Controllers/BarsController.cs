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
                var bar = await _barManager.GetBar(id);
                var barViewModel = BarViewModelMapper.MapBarViewModel(bar);
                barViewModel.ReviewViewModels = (await _barManager.GetAllReviewsByBarID(bar.BarId)).ToVM();
                return View(barViewModel);
            }
            catch (Exception ex)
            {
                return RedirecToActionError("404", ex.Message);
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
            var barReview = reviewViewModel.ToBarDTO();
            if (ModelState.IsValid)
            {
                await _barManager.CreateBarReviewAsync(barReview);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private IActionResult RedirecToActionError(string errorCode, string errorMessage) => RedirectToAction("Error", "Home", new ErrorViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            });
    }
}
