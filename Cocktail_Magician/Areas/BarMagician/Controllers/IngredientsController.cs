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

namespace Cocktail_Magician.Areas.BarMagician.Controllers
{
    [Area("BarMagician")]
    public class IngredientsController : Controller
    {
        private readonly CMContext _context;
        private readonly IIngredientManager _ingredientManager;

        public IngredientsController(CMContext context, IIngredientManager ingredientManager)
        {
            _context = context;
            _ingredientManager = ingredientManager;
        }

        // GET: BarMagician/Ingredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BarMagician/Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IngredientViewModel ingredientToCreate)
        {
            if (ModelState.IsValid)
            {
                var ingredient= new Ingredient
                {
                    Name = ingredientToCreate.Name
                };
                await _ingredientManager.AddIngredientAsync(ingredient);
                return RedirectToAction("Index", "Home");
            }
            return View(ingredientToCreate);
        }

        // GET: BarMagician/Ingredients/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ingredient = await _context.Ingredients.FindAsync(id);
        //    if (ingredient == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ingredient);
        //}

        // POST: BarMagician/Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("IngredientId,Name")] Ingredient ingredient)
        //{
        //    if (id != ingredient.IngredientId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ingredient);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!IngredientExists(ingredient.IngredientId))
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
        //    return View(ingredient);
        //}

        // GET: BarMagician/Ingredients/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ingredient = await _context.Ingredients
        //        .FirstOrDefaultAsync(m => m.IngredientId == id);
        //    if (ingredient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ingredient);
        //}

        // POST: BarMagician/Ingredients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var ingredient = await _context.Ingredients.FindAsync(id);
        //    _context.Ingredients.Remove(ingredient);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool IngredientExists(string id)
        //{
        //    return _context.Ingredients.Any(e => e.IngredientId == id);
        //}
    }
}
