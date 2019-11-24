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
        public static ReviewViewModel ToBarReviewVM(this BarReviewDTO barReviewDTO)
        {
            var vm = new ReviewViewModel
            {
                Id = barReviewDTO.BarId,
                UserId = barReviewDTO.UserId,
                Comment = barReviewDTO.Comment,
                CreatedOn = barReviewDTO.DateCreated,
                Grade = barReviewDTO.Grade,
                UserName = barReviewDTO.UserName,
                UserPicture = barReviewDTO.UserPicture,
            };

            return vm;
        }

        public static BarReviewDTO ToBarReviewDTO(this ReviewViewModel barReviewView)
        {
            var vm = new BarReviewDTO
            {
                BarId = barReviewView.Id,
                UserId = barReviewView.UserId,
                Comment = barReviewView.Comment,
                Grade = barReviewView.Grade,
                UserName = barReviewView.UserName,
                UserPicture = barReviewView.UserPicture,
                DateCreated = barReviewView.CreatedOn
            };

            return vm;
        }

        public static ReviewViewModel ToCocktailReviewVM(this CocktailReviewDTO cocktailReviewDTO)
        {
            var vm = new ReviewViewModel
            {
                Id = cocktailReviewDTO.CocktailId,
                UserId = cocktailReviewDTO.UserId,
                Grade = cocktailReviewDTO.Grade,
                Comment = cocktailReviewDTO.Comment,
                UserName = cocktailReviewDTO.UserName,
                UserPicture = cocktailReviewDTO.UserPicture,
                CreatedOn = cocktailReviewDTO.CreatedOn
            };
            return vm;
        }

        public static CocktailReviewDTO ToCocktailReviewDTO(this ReviewViewModel cocktailReviewView)
        {
            var vm = new CocktailReviewDTO
            {
                CocktailId = cocktailReviewView.Id,
                UserId = cocktailReviewView.UserId,
                Grade = cocktailReviewView.Grade,
                Comment = cocktailReviewView.Comment,
                UserName = cocktailReviewView.UserName,
                UserPicture = cocktailReviewView.UserPicture,
                CreatedOn = cocktailReviewView.CreatedOn
            };
            return vm;
        }

        public static ICollection<ReviewViewModel> ToBarReviewVM(this ICollection<BarReviewDTO> barReviewDTOs)
        {
            var newCollection = barReviewDTOs.Select(br => br.ToBarReviewVM()).ToList();
            return newCollection;
        }

        public static ICollection<BarReviewDTO> ToBarReviewDTO(this ICollection<ReviewViewModel> barReview)
        {
            var newCollection = barReview.Select(br => br.ToBarReviewDTO()).ToList();
            return newCollection;
        }

        public static ICollection<ReviewViewModel> ToCocktailReviewVM(this ICollection<CocktailReviewDTO> cocktailReviewDTOs)
        {
            var newColleciton = cocktailReviewDTOs.Select(c => c.ToCocktailReviewVM()).ToList();
            return newColleciton;
        }

        public static ICollection<CocktailReviewDTO> ToCocktailReviewDTO(this ICollection<ReviewViewModel> cocktailReview)
        {
            var newColleciton = cocktailReview.Select(c => c.ToCocktailReviewDTO()).ToList();
            return newColleciton;
        }
    }
}
