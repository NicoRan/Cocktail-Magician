using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class User : IdentityUser
    {
        public ICollection<Comment> Commetns { get; set; }
    }
}
