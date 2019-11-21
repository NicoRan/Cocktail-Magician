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
                var bar = barView.MapBar();
                await _barManager.CreateBar(bar);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirecToActionError("500", ex.Message);
            }
        }

        //// GET: Bars/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bar = await _context.Bars.FindAsync(id);
        //    if (bar == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(bar);
        //}

        //// POST: Bars/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, Bar bar)
        //{
        //    if (id != bar.BarId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(bar);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BarExists(bar.BarId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(bar);
        //}

        // GET: Bars/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var bar = await _barManager.GetBar(id);
                var barToDelete = BarViewModelMapper.MapBarViewModel(bar);
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var bar = await _barManager.GetBar(id);
                await _barManager.RemoveBar(bar.BarId);
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
