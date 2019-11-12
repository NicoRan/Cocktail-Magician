using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Areas.BarCrower.Models
{
    public class ReviewVeiwModel
    {
        public string Id { get; set; }
        [Display(Name="Comment")]
        public string Commentar { get; set; }
        public int Rating { get; set; }
    }
}
