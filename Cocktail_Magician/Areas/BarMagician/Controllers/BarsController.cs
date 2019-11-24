using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Areas.BarMagician.Models;
using Microsoft.AspNetCore.Authorization;
using Cocktail_Magician.Infrastructure.Mappers;
using System.Linq;
using System.Collections.Generic;

namespace Cocktail_Magician.Areas.BarMagician.Controllers
{
    [Area("BarMagician")]
    public class BarsController : Controller
    {
        private readonly IBarManager _barManager;
        private readonly ICocktailManager _cocktailManager;
        public BarsController(IBarManager barManager, ICocktailManager cocktailManager)
        {
            _barManager = barManager;
            _cocktailManager = cocktailManager;
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
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "500", errorMessage = ex.Message });
            }
        }

        // GET: Bars/Edit/5
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var bar = await _barManager.GetBarForEditAsync(id);
                var barViewModel = bar.ToVMforEdit();
                var createBarViewModel = new CreateBarViewModel();
                createBarViewModel.Bar = barViewModel;
                var allCocktails = await _cocktailManager.GetAllCocktailsAsync();
                var allCocktailsVM = allCocktails.ToCatalogVM();
                foreach (var cocktail in allCocktailsVM)
                {
                    if(!barViewModel.BarCocktailViewModels.Any(bc => bc.CocktailId == cocktail.CocktailId))
                    {
                        createBarViewModel.CocktailsThatCanOffer.Add(cocktail);
                    }
                }

                return View(createBarViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
            }
        }

        // POST: Bars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(List<string> cocktailsToOffer, List<string> cocktailsToRemove, BarViewModel bar)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid bar parameters!");
                return View(bar);
            }
            try
            {
                await _barManager.EditBar(bar.ToDTO(), cocktailsToOffer, cocktailsToRemove);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "500", errorMessage = ex.Message });
            }
        }

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
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
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
                await _barManager.RemoveBar(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
            }
        }
    }
}
