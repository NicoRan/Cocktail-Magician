using Cocktail_Magician.Areas.Identity.Pages.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class LoginAndRegisterModel
    {
        public LoginModel Login { get; set; }
        public RegisterModel Register { get; set; }
    }
}
