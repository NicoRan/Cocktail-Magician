using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IIngredientManager
    {
        Task<Ingredient> AddIngredientAsync(Ingredient ingredient);
        Task<Ingredient> FindIngredientByNameAsync(string name);
        Task<Ingredient> ProvideIngredientAsync(string name);
        Task<ICollection<Ingredient>> GetIngredientsAsync();
    }
}