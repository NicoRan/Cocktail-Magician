using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician.Infrastructure.Mappers;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Cocktail_Magician.Areas.BarMagician.Controllers
{
    [Area("BarMagician")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailManager _cocktailManager;
        private readonly IIngredientManager _ingredientManager;

        public CocktailsController(ICocktailManager cocktailManager, IIngredientManager ingredientManager)
        {
            _cocktailManager = cocktailManager;
            _ingredientManager = ingredientManager;
        }

        // GET: Cocktails/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var createCocktail = new CreateCocktailViewModel();
            createCocktail.Ingredients = new List<string>();
            var listOfIngredients = await _ingredientManager.GetIngredientsAsync();
            foreach (var item in listOfIngredients)
            {
                createCocktail.Ingredients.Add(item.Name);
            }
            return View(createCocktail);
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(CreateCocktailViewModel cocktailToCreate)
        {
            if (string.IsNullOrWhiteSpace(cocktailToCreate.Cocktail?.Name) 
                && string.IsNullOrWhiteSpace(cocktailToCreate.Cocktail?.Picture) 
                && !cocktailToCreate.Cocktail.Ingredients.Any())
            {
                ModelState.AddModelError(string.Empty, "Invalid bar parameters!");
                return RedirectToAction("Create");
            }
            try
            {
                var cocktailToAdd = cocktailToCreate.Cocktail.ToDTO();
                await _cocktailManager.CreateCocktail(cocktailToAdd, cocktailToCreate.Cocktail.Ingredients);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "500", errorMessage = ex.Message });
            }
        }

        //TODO IMPLEMENT EDIT ACTION AND METHOD
        // GET: Cocktails/Edit/5
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var cocktailToEdit = await _cocktailManager.GetCocktailForEdit(id);
                var cocktailToEditVM = new EditCocktailViewModel();
                cocktailToEditVM.Cocktail = cocktailToEdit.ToEditVM();
                var listOfIngredients = await _ingredientManager.GetIngredientsAsync();
                foreach (var ingredient in listOfIngredients)
                {
                    if (!cocktailToEditVM.Cocktail.CocktailIngredients.Any(ci => ci.IngredientId == ingredient.Id))
                    {
                        cocktailToEditVM.IngreddientsThatCanAdd.Add(ingredient.ToVM());
                    }
                }
                return View(cocktailToEditVM);
            }
            catch(Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
            }
        }

        // POST: Cocktails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(List<string> ingredientsToRemove, List<string> ingredientsToAdd, CocktailViewModel cocktail)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError(string.Empty, "Invalid bar parameters!");
            //    return View(cocktail);
            //}
            try
            {
                await _cocktailManager.EditCocktailAsync(cocktail.ToEditDTO(), ingredientsToRemove, ingredientsToAdd);
                return Redirect("/BarCrower/Cocktails/Details/" + cocktail.CocktailId);
            }
            catch(Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
            }
        }

            // GET: Cocktails/Delete/5

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var cocktail = await _cocktailManager.GetCocktail(id);
                var cocktailToDelete = cocktail.ToVM();
                return View(cocktailToDelete);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
            }
        }

        // POST: Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var cocktail = await _cocktailManager.GetCocktail(id);
                await _cocktailManager.RemoveCocktail(cocktail.Id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorAction", "Error", new { errorCode = "404", errorMessage = ex.Message });
            }
        }
    }
}
