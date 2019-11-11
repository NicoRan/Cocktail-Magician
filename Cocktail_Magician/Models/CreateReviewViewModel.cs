using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class CreateReviewViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
    }
}
