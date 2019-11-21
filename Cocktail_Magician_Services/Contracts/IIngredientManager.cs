using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IIngredientManager
    {
        Task AddIngredientAsync(IngredientDTO ingredient);
        Task<Ingredient> FindIngredientByNameAsync(string name);
        //Task<Ingredient> ProvideIngredientAsync(string name);
        Task<ICollection<Ingredient>> GetIngredientsAsync();
    }
}