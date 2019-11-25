using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IBarManager
    {
        Task CreateBar(BarDTO barToCreate);
        Task<BarReviewDTO> CreateBarReviewAsync(BarReviewDTO barReviewDTO);
        Task EditBar(BarDTO bar, ICollection<string> cocktailsToOffer, ICollection<string> cocktailsToRemove);
        Task<ICollection<BarDTO>> GetAllBarsAsync();
        Task<BarDTO> GetBar(string id);
        Task<BarDTO> GetBarForDetails(string id);
        Task<BarDTO> GetBarForEditAsync(string id);
        Task<ICollection<BarDTO>> GetTopRatedBars();
        Task<bool> IsReviewGiven(string barId, string userId);
        Task RemoveBar(string id);
    }
}