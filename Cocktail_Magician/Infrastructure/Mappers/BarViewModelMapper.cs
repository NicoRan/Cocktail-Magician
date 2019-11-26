using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_Services.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class BarViewModelMapper
    {
        public static BarViewModel ToVM(this BarDTO bar)
        {
            var barViewModel = new BarViewModel();
            barViewModel.BarId = bar.Id;
            barViewModel.Address = bar.Address;
            barViewModel.Information = bar.Information;
            barViewModel.Map = bar.MapDirection;
            barViewModel.Name = bar.Name;
            barViewModel.Picture = bar.Picture;
            barViewModel.Rating = bar.Rating;
            barViewModel.BarCocktailViewModels = bar.BarCocktailDTOs.ToVM();
            barViewModel.ReviewViewModels = bar.BarReviewDTOs.ToBarReviewVM();
            return barViewModel;
        }

        public static BarDTO ToDTO(this BarViewModel barView)
        {
            var barDTO = new BarDTO();
            barDTO.Id = barView.BarId;
            barDTO.Address = barView.Address;
            barDTO.Information = barView.Information;
            barDTO.MapDirection = barView.Map;
            barDTO.Name = barView.Name;
            barDTO.Picture = barView.Picture;
            barDTO.Rating = barView.Rating;
            barDTO.BarCocktailDTOs = barView.BarCocktailViewModels != null ? barView.BarCocktailViewModels.ToDTO() : new List<BarCocktailDTO>();
            barDTO.BarReviewDTOs = barView.ReviewViewModels != null ? barView.ReviewViewModels.ToBarReviewDTO() : new List<BarReviewDTO>();
            return barDTO;
        }

        public static BarViewModel ToVMforEdit(this BarDTO bar)
        {
            var barViewModel = new BarViewModel();
            barViewModel.BarId = bar.Id;
            barViewModel.Address = bar.Address;
            barViewModel.Information = bar.Information;
            barViewModel.Map = bar.MapDirection;
            barViewModel.Name = bar.Name;
            barViewModel.Picture = bar.Picture;
            barViewModel.Rating = bar.Rating;
            barViewModel.BarCocktailViewModels = bar.BarCocktailDTOs.ToVM();
            return barViewModel;
        }

        public static BarViewModel ToVMforSearch(this BarDTO bar)
        {
            var barViewModel = new BarViewModel();
            barViewModel.BarId = bar.Id;
            barViewModel.Address = bar.Address;
            barViewModel.Information = bar.Information;
            barViewModel.Map = bar.MapDirection;
            barViewModel.Name = bar.Name;
            barViewModel.Picture = bar.Picture;
            barViewModel.Rating = bar.Rating;
            return barViewModel;
        }

        public static ICollection<BarViewModel> ToVM(this ICollection<BarDTO> bar)
        {
            var newCollection = bar.Select(b => b.ToVM()).ToList();
            return newCollection;
        }
    }
}
