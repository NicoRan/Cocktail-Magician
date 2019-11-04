using Cocktail_Magician.Areas.Identity.Pages.Account;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class AllClassModels
    {
        public LoginModel Login { get; set; }
        public RegisterModel Register { get; set; }
        public LogoutModel Logout { get; set; }
        public BarViewModel Bar { get; set; }
        public IEnumerable<BarViewModel> Index { get; set; }
    }
}
