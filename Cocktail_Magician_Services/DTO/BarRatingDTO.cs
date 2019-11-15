using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_Services.DTO
{
    public class BarRatingDTO
    {
        public string BarId { get; set; }
        public string UserId { get; set; }
        public double Grade { get; set; }
    }
}
