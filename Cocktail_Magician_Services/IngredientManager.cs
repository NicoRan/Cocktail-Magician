using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.Mappers;
using Cocktail_Magician_Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician_Services
{
    public class IngredientManager : IIngredientManager
    {
        private readonly CMContext _context;

        public IngredientManager(CMContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddIngredientAsync(IngredientDTO ingredient)
        {

            if (!CheckIfIngredientExist(ingredient.Name))
            {
                await _context.Ingredients.AddAsync(ingredient.ToEntity());
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Ingredient already exists!");
            }
        }

        public async Task<IngredientDTO> FindIngredientByNameAsync(string name)
        {
            var ingredientToFind = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
            return ingredientToFind.ToDTO();
        }

        public async Task RemoveIngredientById(string ingredientId)
        {
            var result = await _context.Ingredients.FirstOrDefaultAsync(c => c.IngredientId == ingredientId);
            _context.Ingredients.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IngredientDTO> Edit(string ingredientId, string newName)
        {
            var ingredients = await _context.Ingredients.FirstOrDefaultAsync(i => i.IngredientId == ingredientId);
            ingredients.Name = newName;
            await _context.SaveChangesAsync();
            return ingredients.ToDTO();
        }

        public async Task<IngredientDTO> GetIngredientById(string ingredientId)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.IngredientId == ingredientId);
            return ingredient.ToDTO();
        }


        //public async Task<Ingredient> ProvideIngredientAsync(string name)
        //{
        //    if (!CheckIfIngredientExist(name))
        //    {
        //        var ingredient = new Ingredient { Name = name };
        //        await AddIngredientAsync(ingredient);
        //        return ingredient;
        //    }
        //    else
        //    {
        //        var ingredient = await FindIngredientByNameAsync(name);
        //        return ingredient;
        //    }
        //}

        public async Task<ICollection<IngredientDTO>> GetIngredientsAsync()
        {
           var allIngredients = await _context.Ingredients.Include(i => i.CocktailIngredient).Where(c => c.CocktailIngredient.FirstOrDefault(cc => cc.IngredientId == c.IngredientId) == null).ToListAsync();
           return allIngredients.ToDTO();
        }

        private bool CheckIfIngredientExist(string name)
        => _context.Ingredients.Any(i => i.Name == name);
    }
}
