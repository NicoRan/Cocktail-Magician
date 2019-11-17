using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_DB.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class BarViewModelMapper
    {
        public static BarViewModel MapBarViewModel(this Bar bar)
        {
            var barViewModel = new BarViewModel();
            barViewModel.BarId = bar.BarId;
            barViewModel.Address = bar.Address;
            barViewModel.Cocktails = new List<CocktailViewModel>();
            foreach (var cocktail in bar.Cocktails)
            {
                var cocktailViewModel = new CocktailViewModel
                {
                    CocktailId = cocktail.Id,
                    Name = cocktail.Name
                };
                barViewModel.Cocktails.Add(cocktailViewModel);
            }
            barViewModel.Information = bar.Information;
            barViewModel.Map = bar.MapDirections;
            barViewModel.Name = bar.Name;
            barViewModel.Picture = bar.Picture;
            barViewModel.Rating = bar.Rating;
            return barViewModel;
        }

        public static BarViewModel MapTopBarViewModel(this Bar bar)
        {
            var barViewModel = new BarViewModel();
            barViewModel.BarId = bar.BarId;
            barViewModel.Address = bar.Address;
            barViewModel.Information = bar.Information;
            barViewModel.Map = bar.MapDirections;
            barViewModel.Name = bar.Name;
            barViewModel.Picture = bar.Picture;
            barViewModel.Rating = bar.BarRatings.Any(br => br.BarId == bar.BarId) ? bar.BarRatings.Average(br => br.Grade) : 0;
            return barViewModel;
        }

        public static Bar MapBar(this BarViewModel barViewModel)
        {
            var bar = new Bar()
            {
                Address = barViewModel.Address,
                BarId = barViewModel.BarId,
                Information = barViewModel.Information,
                MapDirections = barViewModel.Map,
                Name = barViewModel.Name,
                Picture = barViewModel.Picture
            };
            return bar;
        }
    }
}
