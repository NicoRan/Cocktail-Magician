using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician.Models;
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
                return RedirecToActionError("500", ex.Message);
            }
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
        [HttpGet]
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
                return RedirecToActionError("404", ex.Message);
            }
        }

        // POST: Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
