using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_DB.Models
{
    public class Bar
    {
        public string BarId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public string Picture { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        public bool IsDeleted { get; set; }
    }
}
