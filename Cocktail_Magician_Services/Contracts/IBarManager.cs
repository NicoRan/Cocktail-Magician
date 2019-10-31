using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IBarManager
    {
        Task<Bar> CreateBar(string name, string address, double rating, string picture);
        Task RemoveBar(string id);
        Task<Bar> GetBar(string id);
    }
}