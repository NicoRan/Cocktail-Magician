using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Areas.BarMagician.Models
{
    public class ReviewViewModel
    {
        public string UserName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserPicture { get; set; }
        public string Comment { get; set; }
        public double Grade { get; set; }
    }
}
