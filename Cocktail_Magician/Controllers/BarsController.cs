﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Models;

namespace Cocktail_Magician.Controllers
{
    public class BarsController : Controller
    {
        private readonly IBarManager _barManager;
        private readonly CMContext _context;

        public BarsController(CMContext context, IBarManager barManager)
        {
            _context = context;
            _barManager = barManager;
        }


        // GET: Bars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bar = await _barManager.GetBar(id);
            if (bar == null)
            {
                return NotFound();
            }
            var barViewModel = new BarViewModel(bar);
            var model = new AllClassModels
            {
                Bar = barViewModel
            };
            return View(model);
        }

        // GET: Bars/Create
        public IActionResult Create()
        {
            return View(new AllClassModels());
        }

        // POST: Bars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AllClassModels barView)
        {
            var bar = new Bar()
            {
                Name = barView.Bar.Name,
                Address = barView.Bar.Address,
                Information = barView.Bar.Information,
                Picture = barView.Bar.Picture,
                MapDirections = barView.Bar.Map
            };
            if (ModelState.IsValid)
            {

                await _barManager.CreateBar(bar);
                return RedirectToAction("Index", "Home");
            }
            var model = new AllClassModels();
            
            return View(model);
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
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .FirstOrDefaultAsync(m => m.BarId == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // POST: Bars/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var bar = await _context.Bars.FindAsync(id);
        //    _context.Bars.Remove(bar);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool BarExists(string id)
        {
            return _context.Bars.Any(e => e.BarId == id);
        }
    }
}
