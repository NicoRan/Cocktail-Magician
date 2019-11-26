using Cocktail_Magician_DB.Models;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IBarFactory
    {
        Bar CreateNewBar(string name, string address, string information, string picture, string mapDirection);
    }
}