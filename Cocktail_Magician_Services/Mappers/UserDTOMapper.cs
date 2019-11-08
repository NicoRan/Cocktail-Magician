using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cocktail_Magician_Services.Mappers
{
    public static class UserDTOMapper
    {
        public static UserDTO ToDTO(this User user)
        {
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email
            };
            return userDTO;
        }

        public static ICollection<UserDTO> ToDTO(this ICollection<User> users)
        {
            var newCollection = users.Select(c => c.ToDTO()).ToList();
            return newCollection;
        }
    }
}
