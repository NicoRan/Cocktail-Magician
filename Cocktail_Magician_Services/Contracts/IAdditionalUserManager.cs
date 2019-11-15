using System.Threading.Tasks;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IAdditionalUserManager
    {
        Task AddPictureToProfile(string id, string picture);
        string GetUsersPicture(string id);
    }
}