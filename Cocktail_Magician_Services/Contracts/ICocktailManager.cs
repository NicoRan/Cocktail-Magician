using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ICocktailManager
    {
        Task<Cocktail> CreateCocktail(Cocktail cocktail, List<string> ingredient);
        Task<ICollection<CocktailDTO>> GetTopRatedCocktails();
        Task<Cocktail> GetCocktail(string id);
        Task RemoveCocktail(string id);
        Task<ICollection<CocktailDTO>> GetAllCocktailsAsync();
        Task<CocktailReviewDTO> CreateCocktailReviewAsync(DTO.CocktailReviewDTO cocktailReviewDTO);

        Task<ICollection<CocktailReviewDTO>> GetAllReviewsByCocktailID(string cocktailId);
        Task<bool> IsReviewGiven(string cocktailId, string userId);
    }
}