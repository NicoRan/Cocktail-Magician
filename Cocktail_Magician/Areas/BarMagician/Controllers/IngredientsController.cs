using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Areas.BarMagician.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using Cocktail_Magician_Services.DTO;
using Cocktail_Magician.Infrastructure.Mappers;

namespace Cocktail_Magician.Areas.BarMagician.Controllers
{
    [Area("BarMagician")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientManager _ingredientManager;

        public IngredientsController(IIngredientManager ingredientManager)
        {
            _ingredientManager = ingredientManager;
        }

        // GET: BarMagician/Ingredients/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BarMagician/Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(IngredientViewModel ingredientToCreate)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid bar parameters!");
                return View(ingredientToCreate);
            }
            try
            {
                var ingredient = new IngredientDTO
                {
                    Name = ingredientToCreate.Name
                };
                await _ingredientManager.AddIngredientAsync(ingredient);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "500", errorMessage = ex.Message });
            }
        }

        public async Task<IActionResult> IngredientCatalog()
        {
            return View((await _ingredientManager.GetIngredientsAsync()).ToVM());
        }
        public async Task<IActionResult> Edit(string Id)
        {
            var ingredientDTO = await _ingredientManager.GetIngredientById(Id);
            return View(ingredientDTO.ToVM());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IngredientViewModel ingredientViewModel)
        {
            var result =  (await _ingredientManager.Edit(ingredientViewModel.Id,ingredientViewModel.Name)).ToVM();
            return RedirectToAction("IngredientCatalog", "Ingredients");
        }

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
    }
}
