using System.Threading.Tasks;
using Cocktail_Magician_DB;
using System.Linq;
using Cocktail_Magician_Services.Contracts;

namespace Cocktail_Magician_Services
{
    public class AdditionalUserManager : IAdditionalUserManager
    {
        private readonly CMContext _context;
        public AdditionalUserManager(CMContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method updates the property Picture of the User
        /// </summary>
        /// <param name="id">Id of the User to update picture</param>
        /// <param name="picture">The path of the picture's storage ("in string")</param>
        /// <returns>Task</returns>
        public async Task AddPictureToProfile(string id, string picture)
        {
            var userToModify = _context.Users.SingleOrDefault(u => u.Id == id);
            userToModify.Picture = picture;
            _context.Users.Update(userToModify);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method returns in string the path of the picture's source
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>path of the pisture's storage in string</returns>
        public string GetUsersPicture(string id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user.Picture;
        }
    }
}
