using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IBarManager
    {
        Task<Bar> CreateBar(Bar bar);
        Task RemoveBar(string id);
        Task<Bar> GetBar(string id);
        Task<List<Bar>> GetTopRatedBars();
    }
}