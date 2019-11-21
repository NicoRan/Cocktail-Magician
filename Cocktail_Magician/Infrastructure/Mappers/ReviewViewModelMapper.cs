using Cocktail_Magician.Areas.BarMagician.Models;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Infrastructure.Mappers
{
    public static class ReviewViewModelMapper
    {
        public static ReviewViewModel ToVM(this BarReviewDTO barReviewDTO)
        {
            var vm = new ReviewViewModel
            {
                Id = barReviewDTO.BarId,
                UserId = barReviewDTO.UserId,
                Comment = barReviewDTO.Comment,
                Grade = barReviewDTO.Grade,
                UserName = barReviewDTO.UserName
            };

            return vm;
        } 
        public static ReviewViewModel ToVM(this BarReview barReviewDTO)
        {
            var vm = new ReviewViewModel
            {
                Id = barReviewDTO.BarId,
                UserId = barReviewDTO.UserId,
                Comment = barReviewDTO.Comment,
                Grade = barReviewDTO.Grade,
                UserName = barReviewDTO.User.UserName
            };

            return vm;
        }

        public static ReviewViewModel ToVM(this CocktailReviewDTO cocktailReviewDTO)
        {
            var vm = new ReviewViewModel
            {
                Id = cocktailReviewDTO.CocktailId,
                UserId = cocktailReviewDTO.UserId,
                Grade = cocktailReviewDTO.Grade,
                Comment = cocktailReviewDTO.Comment
            };
            return vm;
        }
        public static ReviewViewModel ToVM(this CocktailReview cocktailReviewDTO)
        {
            var vm = new ReviewViewModel
            {
                Id = cocktailReviewDTO.CocktailId,
                UserId = cocktailReviewDTO.UserId,
                Grade = cocktailReviewDTO.Grade,
                Comment = cocktailReviewDTO.Comment,
                UserName=cocktailReviewDTO.User.UserName
            };
            return vm;
        }

        public static ICollection<ReviewViewModel> ToVM(this ICollection<BarReviewDTO> barReviewDTOs)
        {
            var newCollection = barReviewDTOs.Select(br => br.ToVM()).ToList();
            return newCollection;
        }

        public static ICollection<ReviewViewModel> ToVM(this ICollection<CocktailReviewDTO> cocktailReviewDTOs)
        {
            var newColleciton = cocktailReviewDTOs.Select(c => c.ToVM()).ToList();
            return newColleciton;
        }
    }
}
