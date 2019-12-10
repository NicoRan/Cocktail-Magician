using Cocktail_Magician.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cocktail_Magician.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorAction(string errorCode, string errorMessage)
        {
            var error = new ErrorViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
            return View("Error",error);

        }

  
    }
}