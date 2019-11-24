using Cocktail_Magician.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cocktail_Magician.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorAction(string errorCode, string errorMessage)
        {
            return RedirectToAction("Error", "Home", new ErrorViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            });
        }
    }
}