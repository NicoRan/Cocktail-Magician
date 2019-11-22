using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ICocktailManager
    {
        Task CreateCocktail(CocktailDTO cocktail, List<string> ingredients);
        Task<ICollection<CocktailDTO>> GetTopRatedCocktails();
        Task<CocktailDTO> GetCocktail(string id);
        Task RemoveCocktail(string id);
        Task<ICollection<CocktailDTO>> GetAllCocktailsAsync();
        Task<CocktailReviewDTO> CreateCocktailReviewAsync(DTO.CocktailReviewDTO cocktailReviewDTO);
        Task<CocktailDTO> GetCocktailByName(string name);
        Task<ICollection<CocktailReviewDTO>> GetAllReviewsByCocktailID(string cocktailId);
        Task<bool> IsReviewGiven(string cocktailId, string userId);
    }
}