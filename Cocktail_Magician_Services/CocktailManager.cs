using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocktail_Magician_Services.Mappers;

namespace Cocktail_Magician_Services
{
    public class CocktailManager : ICocktailManager
    {
        private readonly CMContext _context;
        private readonly IIngredientManager _ingredientManager;

        public CocktailManager(IIngredientManager ingredientManager, CMContext context)
        {
            _context = context;
            _ingredientManager = ingredientManager;
        }

        /// <summary>
        /// This method adds the new Cocktail to the DataBase after checking if it does not exists already
        /// </summary>
        /// <param name="cocktail">This is the newly created Cocktail object</param>
        /// <returns>Task</returns>
        public async Task CreateCocktail(CocktailDTO cocktail, List<string> ingredients)
        {
            var cocktailToFind = _context.Cocktails.SingleOrDefault(c => c.Name == cocktail.Name);
            if (cocktailToFind != null)
            {
                throw new InvalidOperationException("Cocktail already exists in the database");
            }

            var cocktailToAdd = cocktail.ToCreateEntity();
            cocktailToAdd.CocktailIngredient = new List<CocktailIngredient>();
            foreach (var ingredient in ingredients)
            {
                var findIngredient = await _ingredientManager.FindIngredientByNameAsync(ingredient);
                if (findIngredient != null)
                {
                    cocktailToAdd.CocktailIngredient.Add(new CocktailIngredient() { Cocktail = cocktailToAdd, Ingredient = findIngredient.ToEntity() });
                }
            }
            cocktailToAdd.Information = CreateCocktailsRecipe(ingredients);
            await _context.Cocktails.AddAsync(cocktailToAdd);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method display the ingredients
        /// </summary>
        /// <param name="ingredients">list of ingredients</param>
        /// <returns>string<CocktailReviewDTO></returns>
        private string CreateCocktailsRecipe(List<string> ingredients)
        {
            return string.Join("," + " ", ingredients.ToArray());
        }

        /// <summary>
        /// This method checks if the User with the given Id has already reviewed the Bar 
        /// with the given Id.
        /// </summary>
        /// <param name="cocktailReviewDTO">CocktailReviewDTO object with the needed data for the review</param>
        /// <returns>Task</returns>
        public async Task CreateCocktailReviewAsync(CocktailReviewDTO cocktailReviewDTO)
        {
            if (cocktailReviewDTO.Grade > 0)
            {
                var cocktailReview = cocktailReviewDTO.ToEntity();

                await _context.CocktailReviews.AddAsync(cocktailReview);
                await _context.SaveChangesAsync();

                await UpdateRating(cocktailReviewDTO.CocktailId);
            }
            else
                await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method finds the ciresponding Cocktail to update and updates its properties with the new values.
        /// Updates the Cocktails recipe by calling the method UpdateInformation.
        /// </summary>
        /// <param name="cocktail">Object of type Cocktail with new values to update</param>
        /// <param name="ingredientsToRemove">List of ingredients names to remove from Recipe</param>
        /// <param name="ingredientsToAdd">List of ingredients names to add to the recipe</param>
        /// <returns>Task</returns>
        public async Task EditCocktailAsync(CocktailDTO cocktail, List<string> ingredientsToRemove, List<string> ingredientsToAdd)
        {
            var cocktailToUpdate = await _context.Cocktails.Include(c => c.CocktailIngredient).FirstOrDefaultAsync(c => c.Id == cocktail.Id);
            cocktailToUpdate.Name = cocktail.Name;
            cocktailToUpdate.Picture = cocktail.Picture;
            if (ingredientsToRemove.Count() > 0 && ingredientsToAdd.Count() > 0)
            {
                foreach (var ingredient in ingredientsToRemove)
                {
                    var ingredientToRemove = await _ingredientManager.FindIngredientByNameAsync(ingredient);
                    _context.CocktailIngredients.Remove(cocktailToUpdate.CocktailIngredient.FirstOrDefault(ci => ci.CocktailId == cocktailToUpdate.Id && ci.IngredientId == ingredientToRemove.Id));
                }
                foreach (var ingredient in ingredientsToAdd)
                {
                    var ingredientToAdd = await _ingredientManager.FindIngredientByNameAsync(ingredient);
                    cocktailToUpdate.CocktailIngredient.Add(new CocktailIngredient()
                    {
                        Cocktail = cocktailToUpdate,
                        Ingredient = ingredientToAdd.ToEntity()
                    });
                }
            }
            else if (ingredientsToRemove.Count() > 0 && ingredientsToAdd.Count() == 0)
            {
                foreach (var ingredient in ingredientsToRemove)
                {
                    var ingredientToRemove = await _ingredientManager.FindIngredientByNameAsync(ingredient);
                    _context.CocktailIngredients.Remove(cocktailToUpdate.CocktailIngredient.FirstOrDefault(ci => ci.CocktailId == cocktailToUpdate.Id && ci.IngredientId == ingredientToRemove.Id));
                }
            }
            else if (ingredientsToRemove.Count() == 0 && ingredientsToAdd.Count() > 0)
            {
                foreach (var ingredient in ingredientsToAdd)
                {
                    var ingredientToAdd = await _ingredientManager.FindIngredientByNameAsync(ingredient);
                    cocktailToUpdate.CocktailIngredient.Add(new CocktailIngredient()
                    {
                        Cocktail = cocktailToUpdate,
                        Ingredient = ingredientToAdd.ToEntity()
                    });
                }
            }

            _context.Cocktails.Update(cocktailToUpdate);
            await _context.SaveChangesAsync();

            await UpdateInformation(cocktailToUpdate.Id);
        }

        /// <summary>
        /// This method ruturn a list with all cocktail that are not set with deleted status
        /// </summary>
        /// <returns>List<Cocktail></returns>
        public async Task<ICollection<CocktailDTO>> GetAllCocktailsAsync()
        {
            var listCocktails = await _context.Cocktails
                .Where(cocktail => !cocktail.IsDeleted)
                .ToListAsync();
            return listCocktails.ToCatalogDTO();
        }

        /// <summary>
        /// This method finds all of the reviews for the given cocktail by it's id 
        /// </summary>
        /// <param name="cocktailId">Id of the cocktail</param>
        /// <param name="cocktailId">Id of the cocktail</param>
        /// <returns>ICollection<CocktailReviewDTO></returns>
        public async Task<ICollection<CocktailReviewDTO>> GetAllReviewsByCocktailID(string cocktailId)
        {
            var reviews = await _context.CocktailReviews.Include(c => c.User).Where(c => c.CocktailId == cocktailId).ToListAsync();

            return reviews.ToDTO();
        }

        /// <summary>
        /// This method finds all cocktails with no deleted status 
        /// </summary>
        /// <param name="id">Id of the Cocktail</param>
        /// <returns>CocktailDTO</returns>
        public async Task<CocktailDTO> GetCocktail(string id)
        {
            try
            {
                var cocktail = await _context.Cocktails
                    .Include(c => c.CocktailIngredient)
                    .Include(c => c.CocktailReviews)
                        .ThenInclude(c => c.User)
                    .Where(c => !c.IsDeleted)
                    .FirstOrDefaultAsync(c => c.Id == id);
                return cocktail.ToDTO();
            }
            catch (Exception)
            {
                throw new Exception("Cocktail not foun!");
            }
        }

        /// <summary>
        /// This method finds the cocktail with a given name
        /// </summary>
        /// <param name="name">name of the Cocktail</param>
        /// <returns>CocktailDTO</returns>
        public async Task<CocktailDTO> GetCocktailByName(string name)
        {
            try
            {
                var result = await _context.Cocktails.SingleOrDefaultAsync(cocktail => cocktail.Name == name);
                var resultDTO = result.ToCatalogDTO();
                return resultDTO;
            }
            catch (Exception)
            {
                throw new Exception("More then one Cocktail found with the specified name!");
            }
        }

        /// <summary>
        /// This method this method finds cocktail by it's 
        /// </summary>
        /// <param name="cocktailId">if of the cocktail</param>
        /// <returns>Cocktail</returns>
        private async Task<Cocktail> GetCocktailEntity(string cocktailId)
        {
            return await _context.Cocktails.FirstOrDefaultAsync(c => c.Id == cocktailId);
        }

        /// <summary>
        /// This method returns a Cocktail with the specified Id and attached Collection CocktailIngredient and
        /// included to the collection is attached Collection of Ingredients.
        /// </summary>
        /// <param name="id">Id of the cocktail</param>
        /// <returns>CocktailDTO</returns>
        public async Task<CocktailDTO> GetCocktailForEdit(string id)
        {
            try
            {
                var cocktail = await _context.Cocktails
                    .Include(c => c.CocktailIngredient).ThenInclude(c => c.Ingredient)
                    .Where(c => !c.IsDeleted)
                    .FirstOrDefaultAsync(c => c.Id == id);
                return cocktail.ToEditDTO();
            }
            catch (Exception)
            {
                throw new Exception("Cocktail not foun!");
            }
        }

        /// <summary>
        /// This method returns the first 4 Cocktails from the Database sorted by the highest rating
        /// and by Name (if Cocktails with the exact same rating).
        /// </summary>
        /// <returns>List of CocktailDTO</returns>
        public async Task<ICollection<CocktailDTO>> GetTopRatedCocktails()
        {
            var topRatedCocktails = await _context.Cocktails
                .OrderByDescending(c => c.Rating)
                .ThenBy(c => c.Name)
                .Where(c => c.IsDeleted == false)
                .Take(4)
                .Select(c => c.ToCatalogDTO())
                .ToListAsync();

            return topRatedCocktails;
        }

        /// <summary>
        /// This method check does the user gave review to the cocktail 
        /// </summary>
        /// <param name="cocktailId">Id of the cocktail</param>
        /// <param name="userId">Id of the user</param>
        /// <returns>Bool</returns>
        public async Task<bool> IsReviewGiven(string cocktailId, string userId)
        {
            return await _context.CocktailReviews.AnyAsync(cr => cr.CocktailId == cocktailId && cr.UserId == userId);
        }

        /// <summary>
        /// This method remove a cocktail by it's id using the method GetCocktailEntity to find the cocktail by id and set it status to deleted
        /// </summary>
        /// <param name="id">Id of the Cocktail</param>
        /// <returns>Task</returns>
        public async Task RemoveCocktail(string id)
        {
            try
            {
                var cocktail = await GetCocktailEntity(id);
                cocktail.IsDeleted = true;
                _context.Cocktails.Update(cocktail);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cocktail to remove not found!");
            }
        }

        /// <summary>
        /// This method finds all CocktailIngredient by Cocktail id from Database with attached table Ingredient
        /// creates a List of strings with ingredients names, calls the method CreateCocktailsRecipe with the same
        /// lists and updates the property Information of the coresponding Cocktail with "id".
        /// </summary>
        /// <param name="id">Id of Cocktail</param>
        /// <returns>Task</returns>
        private async Task UpdateInformation(string id)
        {
            var listIngredients = await _context.CocktailIngredients.Include(ci => ci.Ingredient).Where(ci => ci.CocktailId == id).ToListAsync();
            var result = new List<string>();
            foreach (var ingredient in listIngredients)
            {
                result.Add(ingredient.Ingredient.Name);
            }
            var recipe = CreateCocktailsRecipe(result);
            var cocktailToUpdate = await _context.Cocktails.FirstOrDefaultAsync(c => c.Id == id);
            cocktailToUpdate.Information = recipe;
            _context.Cocktails.Update(cocktailToUpdate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method finds all reviews of the Cocktail and calculates its average rating. After that updates the rating
        /// </summary>
        /// <param name="cocktailId">Id of the Cocktail</param>
        /// <returns>Task</returns>
        private async Task UpdateRating(string cocktailId)
        {
            var rating = await _context.CocktailReviews
                .Where(c => c.CocktailId == cocktailId)
                .AverageAsync(cr => cr.Grade);
            var cocktail = await _context.Cocktails.FindAsync(cocktailId);
            cocktail.Rating = Math.Round(rating, 1);

            _context.Cocktails.Update(cocktail);
            await _context.SaveChangesAsync();
        }
    }
}
