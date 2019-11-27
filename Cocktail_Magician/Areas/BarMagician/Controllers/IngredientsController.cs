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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> IngredientCatalog()
        {
            return View((await _ingredientManager.GetIngredientsAsync()).ToVM());
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string Id)
        {
            var ingredientDTO = await _ingredientManager.GetIngredientById(Id);
            return View(ingredientDTO.ToVM());
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
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

        //POST: BarMagician/Ingredients/Delete/5
        [HttpGet]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            var ingredient = await _ingredientManager.GetIngredientById(id);
            await _ingredientManager.RemoveIngredientById(ingredient.Id);
            
            return RedirectToAction("IngredientCatalog", "Ingredients");
        }
    }
}
