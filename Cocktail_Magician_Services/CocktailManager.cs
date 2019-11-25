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
            cocktailToAdd.Information = CocktailsRecipe(ingredients);
            await _context.Cocktails.AddAsync(cocktailToAdd);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method returns the first 4 Cocktails from the Database sorted by the highest rating
        /// and by Name (if Cocktails with the exact same rating).
        /// </summary>
        /// <returns>List of CocktailDTO</returns>
        public async Task<ICollection<CocktailDTO>> GetTopRatedCocktails()
        {
            var topRatedCocktails = _context.Cocktails
                .Include(b => b.CocktailReviews)
                .ThenInclude(b => b.User)
                .OrderByDescending(c => c.Rating)
                .ThenBy(c => c.Name)
                .Where(c => c.IsDeleted == false)
                .Take(4)
                .Select(c => c.ToDTO());
            
            return await topRatedCocktails.ToListAsync();
        }
        /// <summary>
        /// This method checks if the User with the given Id has already reviewed the Bar 
        /// with the given Id.
        /// </summary>
        /// <param name="cocktailReviewDTO">CocktailReviewDTO object with the needed data for the review</param>
        /// <returns>CocktailReviewDTO</returns>
        public async Task<CocktailReviewDTO> CreateCocktailReviewAsync(CocktailReviewDTO cocktailReviewDTO)
        {
            if (cocktailReviewDTO.Grade != 0)
            {
                var cocktailReview = cocktailReviewDTO.ToEntity();

                await _context.CocktailReviews.AddAsync(cocktailReview);
                await _context.SaveChangesAsync();

                await UpdateRating(cocktailReviewDTO.CocktailId);
            }
            else
            await _context.SaveChangesAsync();

            return cocktailReviewDTO;
        }

        /// <summary>
        /// This method finds all reviews of the Cocktail and calculates its average rating. After that updates the rating
        /// </summary>
        /// <param name="cocktailId">Id of the Cocktail</param>
        /// <returns>Task</returns>
        private async Task UpdateRating(string cocktailId)
        {
            var rating = _context.CocktailReviews
                .Where(c => c.CocktailId == cocktailId)
                .Average(cr => cr.Grade);
            var cocktail = _context.Cocktails.Find(cocktailId);
            cocktail.Rating = Math.Round(rating, 1);

            _context.Cocktails.Update(cocktail);
            await _context.SaveChangesAsync();
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
                        .ThenInclude(c=>c.User)
                    .Where(c => !c.IsDeleted)
                    .FirstOrDefaultAsync(c => c.Id == id);
                return cocktail.ToDTO();
            }
            catch (InvalidOperationException)
            {
                throw new Exception();
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
                var result = await _context.Cocktails.FirstOrDefaultAsync(cocktail => cocktail.Name == name);
                var resultDTO = result.ToEditDTO();
                return resultDTO;
            }
            catch (Exception)
            {
                throw new Exception("Cocktail not foun!");
            }
        }
        /// <summary>
        /// This method remove a cocktail by it's id using the method GetCocktailEntity to find the cocktail by id and set it status to deleted
        /// </summary>
        /// <param name="cocktailId">Id of the Cocktail</param>
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
        /// This method display the ingredients
        /// </summary>
        /// <param name="ingredients">list of ingredients</param>
        /// <returns>string<CocktailReviewDTO></returns>
        private string CocktailsRecipe(List<string> ingredients)
        {
            var result = String.Join(", ", ingredients.ToArray());
            return result;
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
    }
}
