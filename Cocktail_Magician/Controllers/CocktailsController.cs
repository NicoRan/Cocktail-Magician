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
using Cocktail_Magician.Models;

namespace Cocktail_Magician.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly ICocktailManager _cocktailManager;

        public CocktailsController(ICocktailManager cocktailManager)
        {
            _cocktailManager = cocktailManager;
        }

        // GET: Cocktails
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Cocktails.ToListAsync());
        //}

        // GET: Cocktails/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cocktail = await _context.Cocktails
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (cocktail == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cocktail);
        //}

        // GET: Cocktails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CocktailViewModel cocktail)
        {
            if (ModelState.IsValid)
            {
                var cocktailToAdd = new Cocktail()
                {
                    Name = cocktail.Name
                };
                var ingredientsToAdd = new List<Ingredient>();
                foreach (var ingredient in cocktail.Ingredients)
                {
                    Ingredient ingredientToAdd = new Ingredient();
                    ingredientToAdd.Name = ingredient;
                    ingredientsToAdd.Add(ingredientToAdd);
                }
                await _cocktailManager.CreateCocktail(cocktailToAdd, ingredientsToAdd);
                return RedirectToAction("Index", "Home");
            }
            return View(cocktail);
        }

        // GET: Cocktails/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cocktail = await _context.Cocktails.FindAsync(id);
        //    if (cocktail == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(cocktail);
        //}

        // POST: Cocktails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,Name,IsDeleted")] Cocktail cocktail)
        //{
        //    if (id != cocktail.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(cocktail);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CocktailExists(cocktail.Id))
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
        //    return View(cocktail);
        //}

        // GET: Cocktails/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cocktail = await _context.Cocktails
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (cocktail == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cocktail);
        //}

        // POST: Cocktails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var cocktail = await _context.Cocktails.FindAsync(id);
        //    _context.Cocktails.Remove(cocktail);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CocktailExists(string id)
        //{
        //    return _context.Cocktails.Any(e => e.Id == id);
        //}
    }
}
