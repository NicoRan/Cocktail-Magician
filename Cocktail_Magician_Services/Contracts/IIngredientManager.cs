using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IIngredientManager
    {
        Task AddIngredientAsync(IngredientDTO ingredient);
        Task<IngredientDTO> Edit(string ingredientId, string newName);
        //Task<Ingredient> FindIngredientByNameAsync(string name);
        Task<IngredientDTO> FindIngredientByNameAsync(string name);
        Task<IngredientDTO> GetIngredientById(string ingredientId);

        //Task<Ingredient> ProvideIngredientAsync(string name);
        Task<ICollection<IngredientDTO>> GetIngredientsAsync();
        Task RemoveIngredientById(string ingredientId);
    }
}