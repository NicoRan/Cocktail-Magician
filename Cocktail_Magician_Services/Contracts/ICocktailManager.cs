using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ICocktailManager
    {
        Task<Cocktail> CreateCocktail(Cocktail cocktail, List<string> ingredient);
        Task<List<Cocktail>> GetTopRatedCocktails();
        Task<Cocktail> GetCocktail(string id);
        Task RemoveCocktail(string id);
        Task<List<Cocktail>> GetAllCocktailsAsync();
        Task<DTO.CocktailReviewDTO> CreateCocktailReviewAsync(DTO.CocktailReviewDTO cocktailReviewDTO);

        Task<ICollection<CocktailReviewDTO>> GetAllReviewsByCocktailID(string cocktailId);
    }
}