using Cocktail_Magician_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Models
{
    public class BarViewModel
    {
        public BarViewModel(Bar bar)
        {
            BarId = bar.BarId;
            Name = bar.Name;
            Address = bar.Address;
            Rating = bar.Rating;
            Information = bar.Information;
            Picture = bar.Picture;
            Map = bar.MapDirections;
        }
        public string BarId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public string Information { get; set; }
        public string Picture { get; set; }
        public string Map { get; set; }
    }
}
