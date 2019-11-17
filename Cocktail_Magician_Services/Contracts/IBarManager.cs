using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IBarManager
    {
        Task<Bar> CreateBar(Bar bar);
        Task RemoveBar(string id);
        Task<Bar> GetBar(string id);
        Task<List<Bar>> GetTopRatedBars();
        Task<List<Cocktail>> GetBarsOfferedCocktails(string barId);
        Task<BarReviewDTO> CreateBarReviewAsync(BarReviewDTO barReviewDTO);
        Task<List<Bar>> GetAllBarsAsync();
        Task<ICollection<BarReviewDTO>> GetAllReviewsByBarID(string barId);
    }
}