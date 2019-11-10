using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            Filter = new Dictionary<string, bool>();
        }
        public string Criteria { get; set; }
        public string Type { get; set; }
        public Dictionary<string, bool> Filter { get; set; }
    }
}
