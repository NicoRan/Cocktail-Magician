using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IBarManager
    {
        Task CreateBar(BarDTO barToCreate);
        Task EditBar(BarDTO bar, ICollection<string> cocktailsToOffer);
        Task RemoveBar(string id);
        Task<BarDTO> GetBar(string id);
        Task<BarDTO> GetBarForEditAsync(string id);
        Task<BarDTO> GetBarForDetails(string id);
        Task<ICollection<BarDTO>> GetTopRatedBars();
        Task<BarReviewDTO> CreateBarReviewAsync(BarReviewDTO barReviewDTO);
        Task<ICollection<BarDTO>> GetAllBarsAsync();
        Task<ICollection<BarReviewDTO>> GetAllReviewsByBarID(string barId);
        Task<ICollection<BarCocktailDTO>> GetAllBarCocktailsByBarId(string id);
        Task<bool> IsReviewGiven(string barId, string userId);
    }
}