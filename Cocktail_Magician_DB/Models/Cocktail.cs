using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class Cocktail
    {
        public int CocktailId { get; set; }
        public string Name { get; set; }
        public ICollection<Bar> Bars { get; set; }

    }
}
