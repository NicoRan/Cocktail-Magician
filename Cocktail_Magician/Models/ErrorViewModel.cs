using System;

namespace Cocktail_Magician.Models
{
    public class ErrorViewModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(ErrorCode);
    }
}