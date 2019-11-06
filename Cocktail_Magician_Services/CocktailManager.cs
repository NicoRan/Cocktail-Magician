using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            var cocktailToAdd = new Cocktail
            {
                Name = cocktail.Name,
            };
            cocktailToAdd.CocktailIngredient = new List<CocktailIngredient>();
            foreach (var ingredient in ingredients)
            {
                var findIngredient = await _ingredientManager.FindIngredientByNameAsync(ingredient);
                if (findIngredient != null)
                {
                    cocktailToAdd.CocktailIngredient.Add(new CocktailIngredient() { Cocktail = cocktail, Ingredient = findIngredient });
                }
            }

            await _context.Cocktails.AddAsync(cocktailToAdd);
            await _context.SaveChangesAsync();

            return cocktailToAdd;
        }
    }
}
