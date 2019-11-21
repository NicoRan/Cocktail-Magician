using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Areas.BarMagician.Models;
using Microsoft.AspNetCore.Authorization;
using Cocktail_Magician.Infrastructure.Mappers;
using Cocktail_Magician.Models;

namespace Cocktail_Magician.Areas.BarMagician.Controllers
{
    [Area("BarMagician")]
    public class BarsController : Controller
    {
        private readonly IBarManager _barManager;
        public BarsController(IBarManager barManager)
        {
            _barManager = barManager;
        }

        // GET: Bars/Create
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(BarViewModel barView)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid bar parameters!");
                return View(barView);
            }
            try
            {
                var bar = barView.ToDTO();
                await _barManager.CreateBar(bar);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirecToActionError("500", ex.Message);
            }
        }

        // GET: Bars/Edit/5
        //[HttpGet]
        //[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    try
        //    {
        //        var bar = await _barManager.GetBar(id);
        //        var cocktails = await _barManager.GetUnOfferedCocktails(id);
        //        var barViewModel = BarViewModelMapper.MapCreateBarViewModel(bar);
        //        foreach (var cocktail in cocktails)
        //        {
        //            barViewModel.CocktailsThatCanOffer.Add(cocktail);
        //        }
        //        return View(barViewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirecToActionError("404", ex.Message);
        //    }
        //}

        // POST: Bars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> Edit(List<string> cocktailsToOffer, BarViewModel bar)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid bar parameters!");
        //        return View(bar);
        //    }
        //    try
        //    {

        //    }
        //    catch(Exception ex)
        //    {
        //        return RedirecToActionError("500", ex.Message);
        //    }
        //}

        // GET: Bars/Delete/5
        [HttpGet, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var bar = await _barManager.GetBar(id);
                var barToDelete = bar.ToVM();
                return View(barToDelete);
            }
            catch (Exception ex)
            {
                return RedirecToActionError("404", ex.Message);
            }
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var bar = await _barManager.GetBar(id);
                await _barManager.RemoveBar(bar.Id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirecToActionError("404", ex.Message);
            }
        }
        private IActionResult RedirecToActionError(string errorCode, string errorMessage) => RedirectToAction("Error", "Home", new ErrorViewModel
        {
            ErrorCode = errorCode,
            ErrorMessage = errorMessage
        });
    }
}
