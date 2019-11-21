using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;

namespace Cocktail_Magician_Services.Contracts
{
    public interface ISearchManager
    {
        Task<ICollection<BarDTO>> SearchBarsByName(string criteria);
        Task<ICollection<BarDTO>> SearchBarsByAddress(string criteria);
        Task<ICollection<CocktailDTO>> SearchCocktails(string criteria);
    }
}