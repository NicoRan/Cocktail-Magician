using Cocktail_Magician_DB.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task AddPictureToProfile(string id, string picture)
        {
            var userToModify = _context.Users.SingleOrDefault(u => u.Id == id);
            userToModify.Picture = picture;
            _context.Users.Update(userToModify);
            await _context.SaveChangesAsync();
        }

        public string GetUsersPicture(string id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user.Picture;
        }
    }
}
