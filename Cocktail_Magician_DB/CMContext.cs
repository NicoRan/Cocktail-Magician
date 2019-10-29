using Cocktail_Magician_DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB
{
    public class CMContext : IdentityDbContext<User>
    {
        public CMContext()
        {

        }

        public CMContext(DbContextOptions<CMContext> options) 
            : base(options)
        {

        }
    }
}
