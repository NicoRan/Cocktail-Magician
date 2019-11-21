using System;

namespace Cocktail_Magician_Services.DTO
{
    public class BarReviewDTO
    {
        public string BarId { get; set; }
        public string UserId { get; set; }
        public double Grade { get; set; }
        public DateTime DateCreated { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string UserPicture { get; set; }
    }
}
