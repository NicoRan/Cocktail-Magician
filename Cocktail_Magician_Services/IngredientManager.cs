using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
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
        public async Task<Ingredient> AddIngredientAsync(Ingredient ingredient)
        {
            if (!CheckIfIngredientExist(ingredient.Name))
            {
                await _context.Ingredients.AddAsync(ingredient);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Ingredient already exists!");
            }
            return ingredient;
        }

        public async Task<Ingredient> FindIngredientByNameAsync(string name)
        {
            var ingredientToFind = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
            return ingredientToFind;
        }

        public async Task<Ingredient> ProvideIngredientAsync(string name)
        {
            if (!CheckIfIngredientExist(name))
            {
                var ingredient = new Ingredient { Name = name };
                await AddIngredientAsync(ingredient);
                return ingredient;
            }
            else
            {
                var ingredient = await FindIngredientByNameAsync(name);
                return ingredient;
            }
        }

        public async Task<ICollection<Ingredient>> GetIngredientsAsync()
        {
           var allIngredients = await _context.Ingredients.ToListAsync();
            return allIngredients.OrderBy(i => i.Name).ToList();
        }

        private bool CheckIfIngredientExist(string name)
        => _context.Ingredients.Any(i => i.Name == name);

    }
}
