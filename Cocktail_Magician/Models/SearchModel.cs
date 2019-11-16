using System.Collections.Generic;

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
