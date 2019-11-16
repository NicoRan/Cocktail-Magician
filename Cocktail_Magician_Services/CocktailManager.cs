using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Cocktail> CreateCocktail(Cocktail cocktail, List<string> ingredients)
        {
            var cocktailToAdd = cocktail;
            cocktailToAdd.CocktailIngredient = new List<CocktailIngredient>();
            foreach (var ingredient in ingredients)
            {
                var findIngredient = await _ingredientManager.FindIngredientByNameAsync(ingredient);
                if (findIngredient != null)
                {
                    cocktailToAdd.CocktailIngredient.Add(new CocktailIngredient() { Cocktail = cocktail, Ingredient = findIngredient });
                }
            }
            cocktailToAdd.Information = CocktailsRecipe(ingredients);
            await _context.Cocktails.AddAsync(cocktailToAdd);
            await _context.SaveChangesAsync();

            return cocktailToAdd;
        }

        public async Task<List<Cocktail>> GetTopRatedCocktails()
        {
            var cocktails = await _context.Cocktails
                .OrderByDescending(cocktail => cocktail.Rating)
                .ThenBy(cocktail => cocktail.Name)
                .Take(4)
                .ToListAsync();
            return cocktails;
        }

        public async Task<CocktailReviewDTO> CreateCocktailReviewAsync(CocktailReviewDTO cocktailReviewDTO)
        {
            if (cocktailReviewDTO.Grade != 0)
            {
                var cocktailRating = cocktailReviewDTO.ToRatingEntity();

                await _context.CocktailRatings.AddAsync(cocktailRating);
            }

            //TODO cocktailCommentadd
            if (cocktailReviewDTO.Comment != null)
            {
                var cocktailComment = cocktailReviewDTO.ToCommentEntity();

                await _context.CocktailComments.AddAsync(cocktailComment);
            }

            await _context.SaveChangesAsync();

            return cocktailReviewDTO;
        }

        public async Task<Cocktail> GetCocktail(string id)
        {
            try
            {
                var cocktail = await _context.Cocktails.Include(c => c.Ingredients).Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);
                return cocktail;
            }
            catch (InvalidOperationException)
            {
                throw new Exception();
            }
        }

        public async Task RemoveCocktail(string id)
        {
            var cocktailToRemove = await GetCocktail(id);
            if (cocktailToRemove != null)
            {
                cocktailToRemove.IsDeleted = true;
                _context.Cocktails.Update(cocktailToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Cocktail>> GetAllCocktailsAsync()
        {
            return await _context.Cocktails.Include(cocktail => cocktail.Ingredients).Where(cocktail => !cocktail.IsDeleted).ToListAsync(); ;
        }

        private string CocktailsRecipe(List<string> ingredients)
        {
            var result = String.Join(", ", ingredients.ToArray());
            return result;
        }
    }
}
