using Microsoft.AspNetCore.Mvc;
using Cocktail_Magician.Models;

namespace Cocktail_Magician.Infrastructure
{
    public static class ErrorRedirectAction
    {
        private IActionResult RedirecToActionError(string errorCode, string errorMessage)
        {
            return RedirectToAction("Error", "Home", new ErrorViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            });
        }
    }
}
